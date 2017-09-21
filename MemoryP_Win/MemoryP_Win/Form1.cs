using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryP_Win;
using System.Threading;
using CheatEngine;
using System.Text.RegularExpressions;

namespace MemoryP_Win
{

    public partial class Form1 : Form
    {
        String str_01;
        String str_02;
        String str_03;
        String dBugStr;

        int bytesRead = 0;
        byte[] buffer_u = new byte[1];
        byte[] buffer_me = new byte[1];
        byte[] buffer_sEnd = new byte[1];
        private BackgroundWorker bw_01;
        private BackgroundWorker bw_02;
        private BackgroundWorker bw_03;
        static int address_01, address_02, address_03;
        IntPtr processHandle;

        //CheatEng Location
        private CheatEngineLibrary lib;
        private TScanOption scanopt;
        private TVariableType varopt;

        private bool unicode;
        private bool casesensitive;
        private string startscan;
        private string endscan;

        private const int wm_scandone = 0x8000 + 2;

        // 이 영역에서 구분후 제어 하는거같음
        protected override void WndProc(ref Message m)
        {
            int size, i;
            if(m.Msg == wm_scandone)
            {
                
                lvScanner.VirtualListSize = 0;
                lvScanner.Items.Clear();
                btnNextScan.Enabled = true;
                size = lib.iGetBinarySize();

                //이곳에 바이트코드를 처리
                if (varopt == TVariableType.vtArrayOfBytes)
                    lib.iProcessAddress(tbValue1.Text.ToString(), varopt, true, false, size, out dBugStr);

                if (varopt == TVariableType.vtString)
                    if (unicode)
                        lib.iInitFoundList(varopt, size / 16, false, false, false, unicode);
                    else
                        lib.iInitFoundList(varopt, size / 8, false, false, false, unicode);
                else
                    lib.iInitFoundList(varopt, size, false, false, false, unicode);
                if(scanopt != TScanOption.soUnknownValue)
                {
                    i = Math.Min((int)lib.iCountAddressesFound(), 10000000);
                    lvScanner.VirtualListSize = i;
                }
                MessageBox.Show(lib.iCountAddressesFound().ToString());
                timer1.Enabled = true;
            }
            else
            {
                base.WndProc(ref m);
            }
        }



        public Form1()
        {
            InitializeComponent();
            lib = new CheatEngineLibrary();
        }
        //----------------------------------------------------------------------------------- Engine Location Start
        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Library Load Excute
            lib.loadEngine();
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            // Library Unload Excute
            lib.unloadEngine();
        }

        private void btnProcesses_Click(object sender, EventArgs e)
        {
            string processes;
            lib.iGetProcessList(out processes);
            foreach (string process in Regex.Split(processes, "\r\n"))
                ltBox.Items.Add(process);
        }

        // 프로세스를 열어 연결한다.
        private void btnOpenProcess_Click(object sender, EventArgs e)
        {
            string pid = ltBox.SelectedItem.ToString();
            pid = pid.Substring(0, pid.IndexOf('-', 0));
            if(!pid.Equals(""))
            {
                lib.iOpenProcess(pid);
                lib.iInitMemoryScanner(Process.GetCurrentProcess().MainWindowHandle.ToInt32());
                MessageBox.Show("Process opened");
                scanopt = TScanOption.soExactValue;
                varopt = TVariableType.vtDword;
                startscan = "$0000000000000000";
                endscan = "$7fffffffffffffff";
                unicode = false;
                casesensitive = false;
                btnNewScan.Enabled = true;
                btnFirstScan.Enabled = true;
            }
        }

        // 새로 스캔하기위해 기존 스캐너 정보들을 초기화한다.
        private void btnNewScan_Click(object sender, EventArgs e)
        {
            lib.iNewScan();
            btnNextScan.Enabled = false;
            btnFirstScan.Enabled = true;
            lvScanner.VirtualListSize = 0;
        }

        // 첫번째 스캐닝 버튼
        private void btnFirstScan_Click(object sender, EventArgs e)
        {
            TFastScanMethod fastscanmethod;
            Tscanregionpreference writable = Tscanregionpreference.scanInclude,
                executable = Tscanregionpreference.scanDontCare, copyOnWrite = Tscanregionpreference.scanExclude;
            timer1.Enabled = false;
            btnFirstScan.Enabled = false;

            switch(cbWritable.CheckState)
            {
                case CheckState.Unchecked: writable = Tscanregionpreference.scanExclude; break;
                case CheckState.Checked: writable = Tscanregionpreference.scanInclude; break;
                case CheckState.Indeterminate: writable = Tscanregionpreference.scanDontCare; break;
            }

            switch (cbExecutable.CheckState)
            {
                case CheckState.Unchecked: executable = Tscanregionpreference.scanExclude; break;
                case CheckState.Checked: executable = Tscanregionpreference.scanInclude; break;
                case CheckState.Indeterminate: executable = Tscanregionpreference.scanDontCare; break;
            }

            switch (cbCopyOnWrite.CheckState)
            {
                case CheckState.Unchecked: copyOnWrite = Tscanregionpreference.scanExclude; break;
                case CheckState.Checked: copyOnWrite = Tscanregionpreference.scanInclude; break;
                case CheckState.Indeterminate: copyOnWrite = Tscanregionpreference.scanDontCare; break;
            }

            lib.iConfigScanner(writable, executable, copyOnWrite);

            if (cbFastScan.Checked)
            {
                if (rbAlignment.Checked)
                    fastscanmethod = TFastScanMethod.fsmAligned;
                else
                    fastscanmethod = TFastScanMethod.fsmLastDigits;
            }
            else
                fastscanmethod = TFastScanMethod.fsmNotAligned;

            lib.iFirstScan(scanopt, varopt, TRoundingType.rtRounded, tbValue1.Text,
                tbValue2.Text, startscan, endscan, false, false, unicode, casesensitive,
                fastscanmethod, tbAlignment.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lib.iResetValues();
            lvScanner.Refresh();
        }

        private void tbStartScan_TextChanged(object sender, EventArgs e)
        {
            startscan = '$' + tbStartScan.Text;
        }

        private void tbEndScan_TextChanged(object sender, EventArgs e)
        {
            endscan = '$' + tbEndScan.Text;
        }

        // 스캔 타입을 설정한다.
        private void cbScanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbScanType.SelectedIndex)
            {
                case 0: scanopt = TScanOption.soUnknownValue; break;
                case 1: scanopt = TScanOption.soExactValue; break;
                case 2: scanopt = TScanOption.soValueBetween; break;
                case 3: scanopt = TScanOption.soBiggerThan; break;
                case 4: scanopt = TScanOption.soSmallerThan; break;
                case 5: scanopt = TScanOption.soIncreasedValue; break;
                case 6: scanopt = TScanOption.soIncreasedValueBy; break;
                case 7: scanopt = TScanOption.soDecreasedValue; break;
                case 8: scanopt = TScanOption.soIncreasedValueBy; break;
                case 9: scanopt = TScanOption.soChanged; break;
                case 10: scanopt = TScanOption.soUnchanged; break;
                case 11: scanopt = TScanOption.soSearchForThisArray; break;
            }
        }

        // 변수 타입을 설정한다.
        private void cbValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // connect 의 열거형중 다음과 같은 변수를 선택시
            // cbValueType.SeletedIndex << 는 Form의 스피너이다.
            // 해당 스피너에서 다음과 같은 값을 선택하면
            // varopt 에 저장된다.
            switch (cbValueType.SelectedIndex)
            {
                case 0: varopt = TVariableType.vtBinary; break;
                case 1: varopt = TVariableType.vtByte; break;
                case 2: varopt = TVariableType.vtWord; break;
                case 3: varopt = TVariableType.vtDword; break;
                case 4: varopt = TVariableType.vtQword; break;
                case 5: varopt = TVariableType.vtSingle; break;
                case 6: varopt = TVariableType.vtDouble; break;
                case 7: varopt = TVariableType.vtString; break;
                case 8: varopt = TVariableType.vtArrayOfBytes; break;
            }
            // varopt 을 통해 스피너의 값을 받아 분기처리
            switch (varopt)
            {
                case TVariableType.vtBinary:
                case TVariableType.vtByte:
                case TVariableType.vtString:
                case TVariableType.vtUnicodeString:
                case TVariableType.vtArrayOfBytes: tbAlignment.Text = "16"; break;
                case TVariableType.vtByteArrays: tbAlignment.Text = "1"; break;
                case TVariableType.vtWord: tbAlignment.Text = "2"; break;
                    //byte of array type 을 맞춰준다.
                //case TVariableType.vtArrayOfBytes: tbValue1.Text.ToString(); break;
                default: tbAlignment.Text = "4"; break;
            }
        }

        private void cbFastScan_CheckedChanged(object sender, EventArgs e)
        {
            tbAlignment.Enabled = cbFastScan.Checked && cbFastScan.Enabled;
            rbAlignment.Enabled = tbAlignment.Enabled;
            rbLastDigits.Enabled = tbAlignment.Enabled;
        }

        private void lvScanner_SelectedIndexChanged(object sender, RetrieveVirtualItemEventArgs e)
        {
            string address, value;
            try
            {
                ListViewItem lvi = new ListViewItem(); 	// create a listviewitem object
                lib.iGetAddress(e.ItemIndex , out address, out value);
                lvi.Text = address; 		// assign the text to the item
                ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem(); // subitem
                lvsi.Text = value; 	// the subitem text
                lvi.SubItems.Add(lvsi); 			// assign subitem to item
                e.Item = lvi; 		// assign item to event argument's item-property
            }
            catch (Exception ex)
            {
            }
        }

        private void fmScanner_Load(object sender, EventArgs e)
        {
            cbScanType.SelectedIndex = cbScanType.Items.IndexOf("Exact Value");
            cbValueType.SelectedIndex = cbValueType.Items.IndexOf("4 Bytes");

        }

        private void btnNextScan_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btnNextScan.Enabled = false;
            lib.iNextScan(scanopt, TRoundingType.rtRounded, tbValue1.Text, tbValue2.Text,
            false, false, unicode, casesensitive, false, false, "");
        }

        private void cbUnicode_CheckedChanged(object sender, EventArgs e)
        {
            unicode = cbUnicode.Checked;
        }

        private void cbCase_CheckedChanged(object sender, EventArgs e)
        {
            casesensitive = cbCase.Checked;
        }
        //----------------------------------------------------------------------------------- Engine Location End


        public void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        //----------------------------------------------------------------------------------- Push TextValue Button Start
        public void btn_Ur_Scr_Click(object sender, EventArgs e)
        {
            // 1번째 텍스트에 있는 문자열을  str_01에 input
            str_01 = this.ADS_TXT_01.Text.ToString();

            try
            {   // Process 이름을 받아온다RunWorkerAsync
                Process[] _process = Process.GetProcessesByName("PIKAS");
                // Process 첫번째 인스턴스를 받아온다.
                Process process = _process[0];

                Console.WriteLine("main thread: Wroker thread has terminated");

                processHandle = Program.OpenProcess(0x0010, false, process.Id);
                string str = ADS_TXT_01.Text;
                address_01 = Int32.Parse(str, System.Globalization.NumberStyles.HexNumber);

                // <<-  여기서 문제
                if (Program.ReadProcessMemory((int)processHandle, address_01, buffer_u, buffer_u.Length, ref bytesRead) == false)
                {
                    return;
                }
                lable_val_01.Text = BitConverter.ToString(buffer_u);// <- 메모리에서 읽어낸 값을 출력
                //Start the Worker Thread

                bw_01 = new BackgroundWorker();
                // 처리핸들러 등록
                bw_01.DoWork += new DoWorkEventHandler(worker_DoWork_01);
                // 리포트를 받는다.
                bw_01.WorkerReportsProgress = true;
                // 이벤트 핸들러 등록
                bw_01.ProgressChanged += new ProgressChangedEventHandler(worker_Progress_01);
                bw_01.RunWorkerAsync();
            }
            catch (FormatException exf)
            {
                lable_val_01.Text = "필요하다 제대로된 숫자";
            }
            catch (Exception ex)
            {
                lable_val_01.Text = "Error";
            }

        }

        public void btn_Me_Scr_Click(object sender, EventArgs e)
        {
            // 1번째 텍스트에 있는 문자열을  str_01에 input
            str_02 = this.ADS_TXT_02.Text.ToString();

            try
            {   // Process 이름을 받아온다
                Process[] _process = Process.GetProcessesByName("PIKAS");
                // Process 첫번째 인스턴스를 받아온다.
                Process process = _process[0];

                Console.WriteLine("main thread: Wroker thread has terminated");

                processHandle = Program.OpenProcess(0x0010, false, process.Id);
                string str = ADS_TXT_02.Text;
                address_02 = Int32.Parse(str, System.Globalization.NumberStyles.HexNumber);

                if (Program.ReadProcessMemory((int)processHandle, address_02, buffer_me, buffer_me.Length, ref bytesRead) == false)
                {
                    return;
                }
                lable_val_02.Text = BitConverter.ToString(buffer_me);// <- 메모리에서 읽어낸 값을 출력
                //Start the Worker Thread

                bw_02 = new BackgroundWorker();
                // 처리핸들러 등록
                bw_02.DoWork += new DoWorkEventHandler(worker_DoWork_02);
                // 리포트를 받는다.
                bw_02.WorkerReportsProgress = true;
                // 이벤트 핸들러 등록
                bw_02.ProgressChanged += new ProgressChangedEventHandler(worker_Progress_02);
                bw_02.RunWorkerAsync();
            }
            catch (FormatException exf)
            {
                lable_val_02.Text = "필요하다 제대로된 숫자";
            }
            catch (Exception ex)
            {
                lable_val_02.Text = "Error";
            }

        }

        public void btn_Flag_Scr_Click(object sender, EventArgs e)
        {
            // 1번째 텍스트에 있는 문자열을  str_01에 input
            str_03 = this.ADS_TXT_03.Text.ToString();

            try
            {   // Process 이름을 받아온다
                Process[] _process = Process.GetProcessesByName("PIKAS");
                // Process 첫번째 인스턴스를 받아온다.
                Process process = _process[0];

                Console.WriteLine("main thread: Wroker thread has terminated");

                processHandle = Program.OpenProcess(0x0010, false, process.Id);
                string str = ADS_TXT_03.Text;
                address_03 = Int32.Parse(str, System.Globalization.NumberStyles.HexNumber);

                if (Program.ReadProcessMemory((int)processHandle, address_03, buffer_sEnd, buffer_sEnd.Length, ref bytesRead) == false)
                {
                    return;
                }
                lable_val_03.Text = BitConverter.ToString(buffer_sEnd);// <- 메모리에서 읽어낸 값을 출력
                //Start the Worker Thread

                bw_03 = new BackgroundWorker();
                // 처리핸들러 등록
                bw_03.DoWork += new DoWorkEventHandler(worker_DoWork_03);
                // 리포트를 받는다.
                bw_03.WorkerReportsProgress = true;
                // 이벤트 핸들러 등록
                bw_03.ProgressChanged += new ProgressChangedEventHandler(worker_Progress_03);
                bw_03.RunWorkerAsync();
            }
            catch (FormatException exf)
            {
                lable_val_03.Text = "필요하다 제대로된 숫자";
            }
            catch (Exception ex)
            {
                lable_val_03.Text = "Error";
            }
        }
        //----------------------------------------------------------------------------------- Push TextValue Button End

        //----------------------------------------------------------------------------------- Thread Location Start
        public void worker_Progress_01(object sender, ProgressChangedEventArgs e)
        {
            lable_val_01.Text = e.ProgressPercentage.ToString();
        }
        public void worker_DoWork_01(object sender, DoWorkEventArgs e)
        {
            byte[] buf_01 = new byte[5];
            Array.Clear(buf_01, 0, buf_01.Length);
            try
            {
                while (true)
                {
                    Thread.Sleep(500);
                    if (Program.ReadProcessMemory((int)processHandle, address_01, buf_01, buf_01.Length, ref bytesRead) == false)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        int x = BitConverter.ToInt32(buf_01, 0);
                        bw_01.ReportProgress(x);
                    }
                }
            }
            catch (ArgumentNullException en)
            {
                Console.WriteLine(en.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void worker_Progress_02(object sender, ProgressChangedEventArgs e)
        {
            lable_val_02.Text = e.ProgressPercentage.ToString();
        }
        public void worker_DoWork_02(object sender, DoWorkEventArgs e)
        {
            byte[] buf_02 = new byte[5];
            Array.Clear(buf_02, 0, buf_02.Length);
            try
            {
                while (true)
                {
                    Thread.Sleep(500);
                    if (Program.ReadProcessMemory((int)processHandle, address_02, buf_02, buf_02.Length, ref bytesRead) == false)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        int y = BitConverter.ToInt32(buf_02, 0);
                        bw_02.ReportProgress(y);
                    }
                }
            }
            catch (ArgumentNullException en)
            {
                Console.WriteLine(en.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void worker_Progress_03(object sender, ProgressChangedEventArgs e)
        {
            lable_val_03.Text = e.ProgressPercentage.ToString();
        }
        public void worker_DoWork_03(object sender, DoWorkEventArgs e)
        {
            byte[] buf_03 = new byte[5];
            Array.Clear(buf_03, 0, buf_03.Length);
            try
            {
                while (true)
                {
                    Thread.Sleep(500);
                    if (Program.ReadProcessMemory((int)processHandle, address_03, buf_03, buf_03.Length, ref bytesRead) == false)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        int z = BitConverter.ToInt32(buf_03, 0);
                        bw_03.ReportProgress(z);
                    }
                }
            }
            catch (ArgumentNullException en)
            {
                Console.WriteLine(en.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //----------------------------------------------------------------------------------- Thread Location End

        //----------------------------------------------------------------------------------- Getter Location Start
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAutoScan_Click(object sender, EventArgs e)
        {
            //switch (cbValueType.SelectedIndex)
            //{
            //    case 0: varopt = TVariableType.vtBinary; break;
            //    case 1: varopt = TVariableType.vtByte; break;
            //    case 2: varopt = TVariableType.vtWord; break;
            //    case 3: varopt = TVariableType.vtDword; break;
            //    case 4: varopt = TVariableType.vtQword; break;
            //    case 5: varopt = TVariableType.vtSingle; break;
            //    case 6: varopt = TVariableType.vtDouble; break;
            //    case 7: varopt = TVariableType.vtString; break;
            //    case 15: varopt = TVariableType.vtArrayOfBytes; break;
            //}

            //switch (varopt)
            //{
            //    case TVariableType.vtBinary:
            //    case TVariableType.vtByte:
            //    case TVariableType.vtString:
            //    case TVariableType.vtUnicodeString:
            //    case TVariableType.vtByteArrays: tbAlignment.Text = "1"; break;
            //    case TVariableType.vtWord: tbAlignment.Text = "2"; break;
            //    case TVariableType.vtArrayOfBytes: tbValue1.Text.ToString(); break;
            //    default: tbAlignment.Text = "4"; break;
            //}
            
            //MessageBox.Show(TVariableType.vtBinary);
        }

        private void tbValue1_TextChanged(object sender, EventArgs e)
        {

        }

        //----------------------------------------------------------------------------------- Getter Location End
    }
}
