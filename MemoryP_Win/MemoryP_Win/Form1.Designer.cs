namespace MemoryP_Win
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Flag_Scr = new System.Windows.Forms.Button();
            this.ADS_TXT_03 = new System.Windows.Forms.TextBox();
            this.btn_Me_Scr = new System.Windows.Forms.Button();
            this.ADS_TXT_02 = new System.Windows.Forms.TextBox();
            this.btn_Ur_Scr = new System.Windows.Forms.Button();
            this.ADS_TXT_01 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lable_val_03 = new System.Windows.Forms.Label();
            this.lable_val_02 = new System.Windows.Forms.Label();
            this.lable_val_01 = new System.Windows.Forms.Label();
            this.label_03 = new System.Windows.Forms.Label();
            this.label_02 = new System.Windows.Forms.Label();
            this.label_01 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ltBox = new System.Windows.Forms.ListBox();
            this.btnOpenProcess = new System.Windows.Forms.Button();
            this.btnProcesses = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnUnload = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbValueType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbScanType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAutoScan = new System.Windows.Forms.Button();
            this.tbValue2 = new System.Windows.Forms.TextBox();
            this.lable_nUse_02 = new System.Windows.Forms.Label();
            this.tbValue1 = new System.Windows.Forms.TextBox();
            this.lable_nUse_01 = new System.Windows.Forms.Label();
            this.btnNextScan = new System.Windows.Forms.Button();
            this.btnFirstScan = new System.Windows.Forms.Button();
            this.btnNewScan = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbCopyOnWrite = new System.Windows.Forms.CheckBox();
            this.tbEndScan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbExecutable = new System.Windows.Forms.CheckBox();
            this.tbStartScan = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbWritable = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbLastDigits = new System.Windows.Forms.RadioButton();
            this.rbAlignment = new System.Windows.Forms.RadioButton();
            this.tbAlignment = new System.Windows.Forms.TextBox();
            this.cbFastScan = new System.Windows.Forms.CheckBox();
            this.cbCase = new System.Windows.Forms.CheckBox();
            this.cbUnicode = new System.Windows.Forms.CheckBox();
            this.lvScanner = new System.Windows.Forms.ListView();
            this.colAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Flag_Scr);
            this.groupBox1.Controls.Add(this.ADS_TXT_03);
            this.groupBox1.Controls.Add(this.btn_Me_Scr);
            this.groupBox1.Controls.Add(this.ADS_TXT_02);
            this.groupBox1.Controls.Add(this.btn_Ur_Scr);
            this.groupBox1.Controls.Add(this.ADS_TXT_01);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Address Input";
            // 
            // btn_Flag_Scr
            // 
            this.btn_Flag_Scr.Location = new System.Drawing.Point(139, 88);
            this.btn_Flag_Scr.Name = "btn_Flag_Scr";
            this.btn_Flag_Scr.Size = new System.Drawing.Size(75, 23);
            this.btn_Flag_Scr.TabIndex = 5;
            this.btn_Flag_Scr.Text = "시작/종료";
            this.btn_Flag_Scr.UseVisualStyleBackColor = true;
            this.btn_Flag_Scr.Click += new System.EventHandler(this.btn_Flag_Scr_Click);
            // 
            // ADS_TXT_03
            // 
            this.ADS_TXT_03.Location = new System.Drawing.Point(6, 89);
            this.ADS_TXT_03.Name = "ADS_TXT_03";
            this.ADS_TXT_03.Size = new System.Drawing.Size(127, 21);
            this.ADS_TXT_03.TabIndex = 4;
            // 
            // btn_Me_Scr
            // 
            this.btn_Me_Scr.Location = new System.Drawing.Point(139, 55);
            this.btn_Me_Scr.Name = "btn_Me_Scr";
            this.btn_Me_Scr.Size = new System.Drawing.Size(75, 23);
            this.btn_Me_Scr.TabIndex = 3;
            this.btn_Me_Scr.Text = "내 점수";
            this.btn_Me_Scr.UseVisualStyleBackColor = true;
            this.btn_Me_Scr.Click += new System.EventHandler(this.btn_Me_Scr_Click);
            // 
            // ADS_TXT_02
            // 
            this.ADS_TXT_02.Location = new System.Drawing.Point(6, 56);
            this.ADS_TXT_02.Name = "ADS_TXT_02";
            this.ADS_TXT_02.Size = new System.Drawing.Size(127, 21);
            this.ADS_TXT_02.TabIndex = 2;
            // 
            // btn_Ur_Scr
            // 
            this.btn_Ur_Scr.Location = new System.Drawing.Point(139, 22);
            this.btn_Ur_Scr.Name = "btn_Ur_Scr";
            this.btn_Ur_Scr.Size = new System.Drawing.Size(75, 23);
            this.btn_Ur_Scr.TabIndex = 1;
            this.btn_Ur_Scr.Text = "상대점수";
            this.btn_Ur_Scr.UseVisualStyleBackColor = true;
            this.btn_Ur_Scr.Click += new System.EventHandler(this.btn_Ur_Scr_Click);
            // 
            // ADS_TXT_01
            // 
            this.ADS_TXT_01.Location = new System.Drawing.Point(6, 23);
            this.ADS_TXT_01.Name = "ADS_TXT_01";
            this.ADS_TXT_01.Size = new System.Drawing.Size(127, 21);
            this.ADS_TXT_01.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lable_val_03);
            this.groupBox2.Controls.Add(this.lable_val_02);
            this.groupBox2.Controls.Add(this.lable_val_01);
            this.groupBox2.Controls.Add(this.label_03);
            this.groupBox2.Controls.Add(this.label_02);
            this.groupBox2.Controls.Add(this.label_01);
            this.groupBox2.Location = new System.Drawing.Point(12, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 242);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Value Output";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // lable_val_03
            // 
            this.lable_val_03.AutoSize = true;
            this.lable_val_03.Location = new System.Drawing.Point(81, 115);
            this.lable_val_03.Name = "lable_val_03";
            this.lable_val_03.Size = new System.Drawing.Size(38, 12);
            this.lable_val_03.TabIndex = 5;
            this.lable_val_03.Text = "label3";
            // 
            // lable_val_02
            // 
            this.lable_val_02.AutoSize = true;
            this.lable_val_02.Location = new System.Drawing.Point(81, 64);
            this.lable_val_02.Name = "lable_val_02";
            this.lable_val_02.Size = new System.Drawing.Size(38, 12);
            this.lable_val_02.TabIndex = 4;
            this.lable_val_02.Text = "label2";
            // 
            // lable_val_01
            // 
            this.lable_val_01.AutoSize = true;
            this.lable_val_01.Location = new System.Drawing.Point(81, 17);
            this.lable_val_01.Name = "lable_val_01";
            this.lable_val_01.Size = new System.Drawing.Size(38, 12);
            this.lable_val_01.TabIndex = 3;
            this.lable_val_01.Text = "label1";
            // 
            // label_03
            // 
            this.label_03.AutoSize = true;
            this.label_03.Location = new System.Drawing.Point(6, 115);
            this.label_03.Name = "label_03";
            this.label_03.Size = new System.Drawing.Size(59, 12);
            this.label_03.TabIndex = 2;
            this.label_03.Text = "시작/종료";
            // 
            // label_02
            // 
            this.label_02.AutoSize = true;
            this.label_02.Location = new System.Drawing.Point(6, 64);
            this.label_02.Name = "label_02";
            this.label_02.Size = new System.Drawing.Size(45, 12);
            this.label_02.TabIndex = 1;
            this.label_02.Text = "내 점수";
            // 
            // label_01
            // 
            this.label_01.AutoSize = true;
            this.label_01.Location = new System.Drawing.Point(6, 17);
            this.label_01.Name = "label_01";
            this.label_01.Size = new System.Drawing.Size(53, 12);
            this.label_01.TabIndex = 0;
            this.label_01.Text = "상대점수";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ltBox);
            this.groupBox3.Controls.Add(this.btnOpenProcess);
            this.groupBox3.Controls.Add(this.btnProcesses);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Location = new System.Drawing.Point(590, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 584);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Process";
            // 
            // ltBox
            // 
            this.ltBox.AccessibleName = "";
            this.ltBox.ItemHeight = 12;
            this.ltBox.Location = new System.Drawing.Point(6, 150);
            this.ltBox.Name = "ltBox";
            this.ltBox.Size = new System.Drawing.Size(179, 424);
            this.ltBox.TabIndex = 8;
            // 
            // btnOpenProcess
            // 
            this.btnOpenProcess.Location = new System.Drawing.Point(6, 123);
            this.btnOpenProcess.Name = "btnOpenProcess";
            this.btnOpenProcess.Size = new System.Drawing.Size(179, 21);
            this.btnOpenProcess.TabIndex = 7;
            this.btnOpenProcess.Text = "Open Selected Process";
            this.btnOpenProcess.UseVisualStyleBackColor = true;
            this.btnOpenProcess.Click += new System.EventHandler(this.btnOpenProcess_Click);
            // 
            // btnProcesses
            // 
            this.btnProcesses.Location = new System.Drawing.Point(6, 96);
            this.btnProcesses.Name = "btnProcesses";
            this.btnProcesses.Size = new System.Drawing.Size(179, 21);
            this.btnProcesses.TabIndex = 6;
            this.btnProcesses.Text = "Process List";
            this.btnProcesses.UseVisualStyleBackColor = true;
            this.btnProcesses.Click += new System.EventHandler(this.btnProcesses_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnUnload);
            this.groupBox7.Controls.Add(this.btnLoad);
            this.groupBox7.Location = new System.Drawing.Point(6, 14);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(179, 76);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            // 
            // btnUnload
            // 
            this.btnUnload.Location = new System.Drawing.Point(6, 43);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(167, 21);
            this.btnUnload.TabIndex = 2;
            this.btnUnload.Text = "Unload Library";
            this.btnUnload.UseVisualStyleBackColor = true;
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(6, 17);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(167, 21);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load Library";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbValueType);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cbScanType);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.btnAutoScan);
            this.groupBox4.Controls.Add(this.tbValue2);
            this.groupBox4.Controls.Add(this.lable_nUse_02);
            this.groupBox4.Controls.Add(this.tbValue1);
            this.groupBox4.Controls.Add(this.lable_nUse_01);
            this.groupBox4.Controls.Add(this.btnNextScan);
            this.groupBox4.Controls.Add(this.btnFirstScan);
            this.groupBox4.Controls.Add(this.btnNewScan);
            this.groupBox4.Location = new System.Drawing.Point(238, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(346, 182);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Memory Tools";
            // 
            // cbValueType
            // 
            this.cbValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValueType.FormattingEnabled = true;
            this.cbValueType.ItemHeight = 12;
            this.cbValueType.Items.AddRange(new object[] {
            "Binary",
            "Byte",
            "2 Bytes",
            "4 Bytes",
            "8 Bytes",
            "Float",
            "Double",
            "String",
            "Array of Bytes"});
            this.cbValueType.Location = new System.Drawing.Point(103, 145);
            this.cbValueType.Name = "cbValueType";
            this.cbValueType.Size = new System.Drawing.Size(217, 20);
            this.cbValueType.TabIndex = 21;
            this.cbValueType.SelectedIndexChanged += new System.EventHandler(this.cbValueType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "Value Type :";
            // 
            // cbScanType
            // 
            this.cbScanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScanType.FormattingEnabled = true;
            this.cbScanType.ItemHeight = 12;
            this.cbScanType.Items.AddRange(new object[] {
            "UnknownValue",
            "Exact Value",
            "Value Between",
            "Bigger Than",
            "Smaller Than",
            "Increased Value",
            "Increased ValueBy",
            "Decreased Value",
            "Decreased Value By",
            "Changed Value",
            "Unchanged Value",
            "Search for this Array"});
            this.cbScanType.Location = new System.Drawing.Point(103, 112);
            this.cbScanType.Name = "cbScanType";
            this.cbScanType.Size = new System.Drawing.Size(217, 20);
            this.cbScanType.TabIndex = 19;
            this.cbScanType.SelectedIndexChanged += new System.EventHandler(this.cbScanType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "Scan Type :";
            // 
            // btnAutoScan
            // 
            this.btnAutoScan.Location = new System.Drawing.Point(265, 23);
            this.btnAutoScan.Name = "btnAutoScan";
            this.btnAutoScan.Size = new System.Drawing.Size(75, 23);
            this.btnAutoScan.TabIndex = 17;
            this.btnAutoScan.Text = "Auto Scan";
            this.btnAutoScan.UseVisualStyleBackColor = true;
            this.btnAutoScan.Click += new System.EventHandler(this.btnAutoScan_Click);
            // 
            // tbValue2
            // 
            this.tbValue2.Location = new System.Drawing.Point(103, 85);
            this.tbValue2.Name = "tbValue2";
            this.tbValue2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbValue2.Size = new System.Drawing.Size(217, 21);
            this.tbValue2.TabIndex = 16;
            // 
            // lable_nUse_02
            // 
            this.lable_nUse_02.AutoSize = true;
            this.lable_nUse_02.Location = new System.Drawing.Point(12, 88);
            this.lable_nUse_02.Name = "lable_nUse_02";
            this.lable_nUse_02.Size = new System.Drawing.Size(85, 12);
            this.lable_nUse_02.TabIndex = 15;
            this.lable_nUse_02.Text = "Search Val 2 :";
            // 
            // tbValue1
            // 
            this.tbValue1.Location = new System.Drawing.Point(103, 57);
            this.tbValue1.Name = "tbValue1";
            this.tbValue1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbValue1.Size = new System.Drawing.Size(217, 21);
            this.tbValue1.TabIndex = 14;
            this.tbValue1.TextChanged += new System.EventHandler(this.tbValue1_TextChanged);
            // 
            // lable_nUse_01
            // 
            this.lable_nUse_01.AutoSize = true;
            this.lable_nUse_01.Location = new System.Drawing.Point(12, 60);
            this.lable_nUse_01.Name = "lable_nUse_01";
            this.lable_nUse_01.Size = new System.Drawing.Size(85, 12);
            this.lable_nUse_01.TabIndex = 13;
            this.lable_nUse_01.Text = "Search Val 1 :";
            // 
            // btnNextScan
            // 
            this.btnNextScan.Location = new System.Drawing.Point(168, 22);
            this.btnNextScan.Name = "btnNextScan";
            this.btnNextScan.Size = new System.Drawing.Size(75, 23);
            this.btnNextScan.TabIndex = 2;
            this.btnNextScan.Text = "Next Scan";
            this.btnNextScan.UseVisualStyleBackColor = true;
            this.btnNextScan.Click += new System.EventHandler(this.btnNextScan_Click);
            // 
            // btnFirstScan
            // 
            this.btnFirstScan.Location = new System.Drawing.Point(87, 22);
            this.btnFirstScan.Name = "btnFirstScan";
            this.btnFirstScan.Size = new System.Drawing.Size(75, 23);
            this.btnFirstScan.TabIndex = 1;
            this.btnFirstScan.Text = "First Scan";
            this.btnFirstScan.UseVisualStyleBackColor = true;
            this.btnFirstScan.Click += new System.EventHandler(this.btnFirstScan_Click);
            // 
            // btnNewScan
            // 
            this.btnNewScan.Location = new System.Drawing.Point(6, 22);
            this.btnNewScan.Name = "btnNewScan";
            this.btnNewScan.Size = new System.Drawing.Size(75, 23);
            this.btnNewScan.TabIndex = 0;
            this.btnNewScan.Text = "New Scan";
            this.btnNewScan.UseVisualStyleBackColor = true;
            this.btnNewScan.Click += new System.EventHandler(this.btnNewScan_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbCopyOnWrite);
            this.groupBox5.Controls.Add(this.tbEndScan);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.cbExecutable);
            this.groupBox5.Controls.Add(this.tbStartScan);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.cbWritable);
            this.groupBox5.Location = new System.Drawing.Point(238, 200);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(346, 98);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Memory Scan option";
            // 
            // cbCopyOnWrite
            // 
            this.cbCopyOnWrite.AutoSize = true;
            this.cbCopyOnWrite.Location = new System.Drawing.Point(209, 76);
            this.cbCopyOnWrite.Name = "cbCopyOnWrite";
            this.cbCopyOnWrite.Size = new System.Drawing.Size(97, 16);
            this.cbCopyOnWrite.TabIndex = 19;
            this.cbCopyOnWrite.Text = "CopyOnWrite";
            this.cbCopyOnWrite.ThreeState = true;
            this.cbCopyOnWrite.UseVisualStyleBackColor = true;
            // 
            // tbEndScan
            // 
            this.tbEndScan.Location = new System.Drawing.Point(103, 49);
            this.tbEndScan.Name = "tbEndScan";
            this.tbEndScan.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbEndScan.Size = new System.Drawing.Size(217, 21);
            this.tbEndScan.TabIndex = 16;
            this.tbEndScan.Text = "7FFFFFFFFFFFFFFF";
            this.tbEndScan.TextChanged += new System.EventHandler(this.tbEndScan_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "Stop :";
            // 
            // cbExecutable
            // 
            this.cbExecutable.AutoSize = true;
            this.cbExecutable.Checked = true;
            this.cbExecutable.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.cbExecutable.Location = new System.Drawing.Point(104, 76);
            this.cbExecutable.Name = "cbExecutable";
            this.cbExecutable.Size = new System.Drawing.Size(87, 16);
            this.cbExecutable.TabIndex = 18;
            this.cbExecutable.Text = "Executable";
            this.cbExecutable.ThreeState = true;
            this.cbExecutable.UseVisualStyleBackColor = true;
            // 
            // tbStartScan
            // 
            this.tbStartScan.Location = new System.Drawing.Point(103, 22);
            this.tbStartScan.Name = "tbStartScan";
            this.tbStartScan.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbStartScan.Size = new System.Drawing.Size(217, 21);
            this.tbStartScan.TabIndex = 14;
            this.tbStartScan.Text = "0000000000000000";
            this.tbStartScan.TextChanged += new System.EventHandler(this.tbStartScan_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Start :";
            // 
            // cbWritable
            // 
            this.cbWritable.AutoSize = true;
            this.cbWritable.Checked = true;
            this.cbWritable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWritable.Location = new System.Drawing.Point(22, 76);
            this.cbWritable.Name = "cbWritable";
            this.cbWritable.Size = new System.Drawing.Size(68, 16);
            this.cbWritable.TabIndex = 17;
            this.cbWritable.Text = "Writable";
            this.cbWritable.ThreeState = true;
            this.cbWritable.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbLastDigits);
            this.groupBox6.Controls.Add(this.rbAlignment);
            this.groupBox6.Controls.Add(this.tbAlignment);
            this.groupBox6.Controls.Add(this.cbFastScan);
            this.groupBox6.Controls.Add(this.cbCase);
            this.groupBox6.Controls.Add(this.cbUnicode);
            this.groupBox6.Location = new System.Drawing.Point(238, 304);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(346, 86);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Optional options";
            // 
            // rbLastDigits
            // 
            this.rbLastDigits.AutoSize = true;
            this.rbLastDigits.Enabled = false;
            this.rbLastDigits.Location = new System.Drawing.Point(213, 41);
            this.rbLastDigits.Name = "rbLastDigits";
            this.rbLastDigits.Size = new System.Drawing.Size(71, 16);
            this.rbLastDigits.TabIndex = 22;
            this.rbLastDigits.Text = "Last bits";
            this.rbLastDigits.UseVisualStyleBackColor = true;
            // 
            // rbAlignment
            // 
            this.rbAlignment.AutoSize = true;
            this.rbAlignment.Checked = true;
            this.rbAlignment.Location = new System.Drawing.Point(213, 20);
            this.rbAlignment.Name = "rbAlignment";
            this.rbAlignment.Size = new System.Drawing.Size(79, 16);
            this.rbAlignment.TabIndex = 21;
            this.rbAlignment.TabStop = true;
            this.rbAlignment.Text = "Alignment";
            this.rbAlignment.UseVisualStyleBackColor = true;
            // 
            // tbAlignment
            // 
            this.tbAlignment.Location = new System.Drawing.Point(137, 18);
            this.tbAlignment.Name = "tbAlignment";
            this.tbAlignment.ReadOnly = true;
            this.tbAlignment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbAlignment.Size = new System.Drawing.Size(70, 21);
            this.tbAlignment.TabIndex = 20;
            // 
            // cbFastScan
            // 
            this.cbFastScan.AutoSize = true;
            this.cbFastScan.Checked = true;
            this.cbFastScan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFastScan.Location = new System.Drawing.Point(17, 42);
            this.cbFastScan.Name = "cbFastScan";
            this.cbFastScan.Size = new System.Drawing.Size(81, 16);
            this.cbFastScan.TabIndex = 2;
            this.cbFastScan.Text = "Fast Scan";
            this.cbFastScan.UseVisualStyleBackColor = true;
            this.cbFastScan.CheckedChanged += new System.EventHandler(this.cbFastScan_CheckedChanged);
            // 
            // cbCase
            // 
            this.cbCase.AutoSize = true;
            this.cbCase.Location = new System.Drawing.Point(17, 64);
            this.cbCase.Name = "cbCase";
            this.cbCase.Size = new System.Drawing.Size(109, 16);
            this.cbCase.TabIndex = 1;
            this.cbCase.Text = "Case Sensitive";
            this.cbCase.UseVisualStyleBackColor = true;
            this.cbCase.CheckedChanged += new System.EventHandler(this.cbCase_CheckedChanged);
            // 
            // cbUnicode
            // 
            this.cbUnicode.AutoSize = true;
            this.cbUnicode.Location = new System.Drawing.Point(17, 20);
            this.cbUnicode.Name = "cbUnicode";
            this.cbUnicode.Size = new System.Drawing.Size(70, 16);
            this.cbUnicode.TabIndex = 0;
            this.cbUnicode.Text = "Unicode";
            this.cbUnicode.UseVisualStyleBackColor = true;
            this.cbUnicode.CheckedChanged += new System.EventHandler(this.cbUnicode_CheckedChanged);
            // 
            // lvScanner
            // 
            this.lvScanner.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAddress,
            this.colValue});
            this.lvScanner.Location = new System.Drawing.Point(12, 396);
            this.lvScanner.Name = "lvScanner";
            this.lvScanner.Size = new System.Drawing.Size(572, 200);
            this.lvScanner.TabIndex = 21;
            this.lvScanner.UseCompatibleStateImageBehavior = false;
            this.lvScanner.View = System.Windows.Forms.View.Details;
            this.lvScanner.VirtualMode = true;
            this.lvScanner.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvScanner_SelectedIndexChanged);
            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 183;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 183;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 611);
            this.Controls.Add(this.lvScanner);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "MemoryEngine";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ADS_TXT_03;
        private System.Windows.Forms.Button btn_Me_Scr;
        private System.Windows.Forms.TextBox ADS_TXT_02;
        private System.Windows.Forms.Button btn_Ur_Scr;
        private System.Windows.Forms.TextBox ADS_TXT_01;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label_01;
        private System.Windows.Forms.Label label_03;
        private System.Windows.Forms.Label label_02;
        private System.Windows.Forms.Label lable_val_03;
        private System.Windows.Forms.Label lable_val_02;
        private System.Windows.Forms.Label lable_val_01;
        public System.Windows.Forms.Button btn_Flag_Scr;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbValue2;
        private System.Windows.Forms.Label lable_nUse_02;
        private System.Windows.Forms.TextBox tbValue1;
        private System.Windows.Forms.Label lable_nUse_01;
        private System.Windows.Forms.Button btnNextScan;
        private System.Windows.Forms.Button btnFirstScan;
        private System.Windows.Forms.Button btnNewScan;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbCopyOnWrite;
        private System.Windows.Forms.TextBox tbEndScan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbExecutable;
        private System.Windows.Forms.TextBox tbStartScan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbWritable;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbLastDigits;
        private System.Windows.Forms.RadioButton rbAlignment;
        private System.Windows.Forms.TextBox tbAlignment;
        private System.Windows.Forms.CheckBox cbFastScan;
        private System.Windows.Forms.CheckBox cbCase;
        private System.Windows.Forms.CheckBox cbUnicode;
        private System.Windows.Forms.ListBox ltBox;
        private System.Windows.Forms.Button btnOpenProcess;
        private System.Windows.Forms.Button btnProcesses;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnUnload;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAutoScan;
        private System.Windows.Forms.ListView lvScanner;
        private System.Windows.Forms.ColumnHeader colAddress;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cbValueType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbScanType;
        private System.Windows.Forms.Label label3;
    }
}

