using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheatEngine
{
    // 열거형
    // 지정된 수가 없으면 차례대로 0부터 상수가 할당된다
    enum TScanOption { soUnknownValue = 0, soExactValue = 1, soValueBetween = 2, soBiggerThan = 3, soSmallerThan = 4, soIncreasedValue = 5, soIncreasedValueBy = 6, soDecreasedValue = 7, soDecreasedValueBy = 8, soChanged = 9, soUnchanged = 10, soSearchForThisArray = 11, soCustom };
    enum TScanType { stNewScan, stFirstScan, stNextScan };
    enum TRoundingType { rtRounded = 0, rtExtremerounded = 1, rtTruncated = 2 };
    enum TVariableType { vtByte = 0, vtWord = 1, vtDword = 2, vtQword = 3, vtSingle = 4, vtDouble = 5, vtString = 6, vtUnicodeString = 7, vtByteArray = 8, vtBinary = 9, vtAll = 10, vtAutoAssembler = 11, vtPointer = 12, vtCustom = 13, vtGrouped = 14, vtByteArrays = 15 , vtArrayOfBytes = 16}; //all ,grouped and MultiByteArray are special types
    enum TCustomScanType { cstNone, cstAutoAssembler, cstCPP, cstDLLFunction };
    enum TFastScanMethod { fsmNotAligned = 0, fsmAligned = 1, fsmLastDigits = 2 };
    enum Tscanregionpreference { scanDontCare, scanExclude, scanInclude };

    // CheatEngine Lib
    class CheatEngineLibrary
    {
        // 정적 호출
        // Static invocations.
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DllImport : C# 코드에서는 다음의 두 가지 방법으로 관리되지 않는 코드를 직접 호출할 수 있습니다.
        // DLL에서 노출된 함수를 직접 호출합니다.
        // COM 개체에서 인터페이스 메서드를 호출합니다.
        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        extern static IntPtr LoadLibraryW(String strLib);

        [DllImport("kernel32.dll")]
        extern static IntPtr LoadLibrary(String strLib);

        [DllImport("kernel32.dll")]
        extern static int FreeLibrary(IntPtr iModule);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        
        // 사용자로 부터 GetProcAddress 메서드의 파라미터를 받는다.
        // 객체를 가져오는것과 비슷하다고 생각하면 된다.
        extern static IntPtr GetProcAddress(IntPtr iModule, String strProcName);

        // 대리자를 지정한다.
        // MarshalAs ?
        // 컴퓨터에서의 마샬링이란?
        // 하나이상의 프로그램 또는 연속되어 있지않은 저장공간으로부터 데이터를 모은다음
        // 데이터 들을 메세지 버퍼에 집어넣고 
        // 특정 수신기나 프로그램 인터페이스에 맞도록 그 데이터를 조직화 하거나
        // 미리 만들어진 데이터 형식으로 변환하는 과정을 말한다.

       //  IGetProcessList의 데이터를 UnmanagedType.BStr로 변환시킨다. 
        public delegate void IGetProcessList([MarshalAs(UnmanagedType.BStr)] out string processes);
        public delegate void IOpenProcess([MarshalAs(UnmanagedType.BStr)] string pid);

        public delegate void IResetTable();
        public delegate void IAddScript([MarshalAs(UnmanagedType.BStr)] string name, [MarshalAs(UnmanagedType.BStr)] string script);
        public delegate void IActivateRecord(int id, bool activate);
        public delegate void IRemoveRecord(int id);
        public delegate void IApplyFreeze();

        public delegate void IAddAddressManually([MarshalAs(UnmanagedType.BStr)] string initialaddress,
        TVariableType vartype);
        public delegate void IGetValue(int id, [MarshalAs(UnmanagedType.BStr)] out string value);
        public delegate void ISetValue(int id, [MarshalAs(UnmanagedType.BStr)] string value, bool freezer);
        public delegate void IProcessAddress([MarshalAs(UnmanagedType.BStr)] string address, TVariableType vartype,
         bool showashexadecimal, bool showAsSigned, int bytesize, [MarshalAs(UnmanagedType.BStr)] out string value);

        public delegate void IInitMemoryScanner(int handle);
        public delegate void INewScan();
        public delegate void IConfigScanner(Tscanregionpreference scanWritable, Tscanregionpreference scanExecutable, Tscanregionpreference scanCopyOnWrite);

        // 여기서 속성 정의
        //  첫 스캐닝을 여기서 시작
        public delegate void IFirstScan(TScanOption scanOption, TVariableType variableType,
  TRoundingType roundingtype, [MarshalAs(UnmanagedType.BStr)] string scanvalue1,
   [MarshalAs(UnmanagedType.BStr)] string scanvalue2, [MarshalAs(UnmanagedType.BStr)] string startaddress,
   [MarshalAs(UnmanagedType.BStr)] string stopaddress, bool hexadecimal, bool binaryStringAsDecimal,
   bool unicode, bool casesensitive, TFastScanMethod fastscanmethod,
   [MarshalAs(UnmanagedType.BStr)] string fastscanparameter);
        // 두번째 스캐닝도 여기서 시작
        public delegate void INextScan(TScanOption scanOption, TRoundingType roundingtype, [MarshalAs(UnmanagedType.BStr)] string scanvalue1,
 [MarshalAs(UnmanagedType.BStr)] string scanvalue2, bool hexadecimal, bool binaryStringAsDecimal,
 bool unicode, bool casesensitive, bool percentage, bool compareToSavedScan, [MarshalAs(UnmanagedType.BStr)] string savedscanname);

        public delegate Int64 ICountAddressesFound();
        public delegate void IGetAddress(Int64 index, [MarshalAs(UnmanagedType.BStr)] out string address, [MarshalAs(UnmanagedType.BStr)] out string value);
        public delegate void IInitFoundList(TVariableType vartype, int varlength, bool hexadecimal, bool signed, bool binaryasdecimal, bool unicode);
        public delegate void IResetValues();
        public delegate int IGetBinarySize();

        private IntPtr libInst;

        public IGetProcessList iGetProcessList;
        public IOpenProcess iOpenProcess;

        public IResetTable iResetTable;
        public IAddScript iAddScript;
        public IRemoveRecord iRemoveRecord;
        public IActivateRecord iActivateRecord;
        public IApplyFreeze iApplyFreeze;

        public IAddAddressManually iAddAddressManually;
        public IGetValue iGetValue;
        public ISetValue iSetValue;
        public IProcessAddress iProcessAddress;

        public IInitMemoryScanner iInitMemoryScanner;
        public INewScan iNewScan;
        public IConfigScanner iConfigScanner;
        public IFirstScan iFirstScan;
        public INextScan iNextScan;
        public ICountAddressesFound iCountAddressesFound;
        public IGetAddress iGetAddress;
        public IInitFoundList iInitFoundList;
        public IResetValues iResetValues;
        public IGetBinarySize iGetBinarySize;

        private void loadFunctions()
        {
            IntPtr pGetProcessList = GetProcAddress(libInst, "IGetProcessList");
            IntPtr pOpenProcess = GetProcAddress(libInst, "IOpenProcess");

            IntPtr pResetTable = GetProcAddress(libInst, "IResetTable");
            IntPtr pAddScript = GetProcAddress(libInst, "IAddScript");
            IntPtr pRemoveRecord = GetProcAddress(libInst, "IRemoveRecord");
            IntPtr pActivateRecord = GetProcAddress(libInst, "IActivateRecord");
            IntPtr pApplyFreeze = GetProcAddress(libInst, "IApplyFreeze");

            IntPtr pAddAddressManually = GetProcAddress(libInst, "IAddAddressManually");
            IntPtr pGetValue = GetProcAddress(libInst, "IGetValue");
            IntPtr pSetValue = GetProcAddress(libInst, "ISetValue");
            IntPtr pProcessAddress = GetProcAddress(libInst, "IProcessAddress");

            IntPtr pInitMemoryScanner = GetProcAddress(libInst, "IInitMemoryScanner");
            IntPtr pNewScan = GetProcAddress(libInst, "INewScan");
            IntPtr pConfigScanner = GetProcAddress(libInst, "IConfigScanner");
            IntPtr pFirstScan = GetProcAddress(libInst, "IFirstScan");
            IntPtr pNextScan = GetProcAddress(libInst, "INextScan");
            IntPtr pCountAddressesFound = GetProcAddress(libInst, "ICountAddressesFound");
            IntPtr pGetAddress = GetProcAddress(libInst, "IGetAddress");
            IntPtr pInitFoundList = GetProcAddress(libInst, "IInitFoundList");
            IntPtr pResetValues = GetProcAddress(libInst, "IResetValues");
            IntPtr pGetBinarySize = GetProcAddress(libInst, "IGetBinarySize");

            iGetProcessList = (IGetProcessList)Marshal.GetDelegateForFunctionPointer(pGetProcessList, typeof(IGetProcessList));
            iOpenProcess = (IOpenProcess)Marshal.GetDelegateForFunctionPointer(pOpenProcess, typeof(IOpenProcess));

            iResetTable = (IResetTable)Marshal.GetDelegateForFunctionPointer(pResetTable, typeof(IResetTable));
            iAddScript = (IAddScript)Marshal.GetDelegateForFunctionPointer(pAddScript, typeof(IAddScript));
            iRemoveRecord = (IRemoveRecord)Marshal.GetDelegateForFunctionPointer(pRemoveRecord, typeof(IRemoveRecord));
            iActivateRecord = (IActivateRecord)Marshal.GetDelegateForFunctionPointer(pActivateRecord, typeof(IActivateRecord));
            iApplyFreeze = (IApplyFreeze)Marshal.GetDelegateForFunctionPointer(pApplyFreeze, typeof(IApplyFreeze));

            iAddAddressManually = (IAddAddressManually)Marshal.GetDelegateForFunctionPointer(pAddAddressManually, typeof(IAddAddressManually));
            iGetValue = (IGetValue)Marshal.GetDelegateForFunctionPointer(pGetValue, typeof(IGetValue));
            iSetValue = (ISetValue)Marshal.GetDelegateForFunctionPointer(pSetValue, typeof(ISetValue));
            iProcessAddress = (IProcessAddress)Marshal.GetDelegateForFunctionPointer(pProcessAddress, typeof(IProcessAddress));

            iInitMemoryScanner = (IInitMemoryScanner)Marshal.GetDelegateForFunctionPointer(pInitMemoryScanner, typeof(IInitMemoryScanner));
            iNewScan = (INewScan)Marshal.GetDelegateForFunctionPointer(pNewScan, typeof(INewScan));
            iConfigScanner = (IConfigScanner)Marshal.GetDelegateForFunctionPointer(pConfigScanner, typeof(IConfigScanner));
            iFirstScan = (IFirstScan)Marshal.GetDelegateForFunctionPointer(pFirstScan, typeof(IFirstScan));
            iNextScan = (INextScan)Marshal.GetDelegateForFunctionPointer(pNextScan, typeof(INextScan));

            iCountAddressesFound = (ICountAddressesFound)Marshal.GetDelegateForFunctionPointer(pCountAddressesFound, typeof(ICountAddressesFound));
            iGetAddress = (IGetAddress)Marshal.GetDelegateForFunctionPointer(pGetAddress, typeof(IGetAddress));
            iInitFoundList = (IInitFoundList)Marshal.GetDelegateForFunctionPointer(pInitFoundList, typeof(IInitFoundList));
            iResetValues = (IResetValues)Marshal.GetDelegateForFunctionPointer(pResetValues, typeof(IResetValues));
            iGetBinarySize = (IGetBinarySize)Marshal.GetDelegateForFunctionPointer(pGetBinarySize, typeof(IGetBinarySize));
        }

        public void loadEngine()
        {
            string Dbg;
            // 64bit 시스템일시 라이브러리 로드
            if (Environment.Is64BitProcess)
            {
                libInst = LoadLibraryW("ce-lib64.dll");
                Dbg = "ce-lib64.dll";
            }
            // 아니면 32bit 라이브러리 로드
            else
            {
                libInst = LoadLibrary("ce-lib32.dll");
                Dbg = "ce-lib32.dll";
            }
            // 라이브러리가 로드되면 loadFunctions를 통해 모든 포인터 호출
            if (libInst != IntPtr.Zero)
            {
                MessageBox.Show("loaded");
                loadFunctions();
            }
            else MessageBox.Show("error, can't load the library: " + this.libInst.ToString() + "\nTarget with: "+Dbg);

        }

        // 라이브러리를 언로드
        public void unloadEngine()
        {
            FreeLibrary(libInst);
        }

    }
}
