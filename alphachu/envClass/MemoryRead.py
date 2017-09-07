'''

1. process 핸들과 2. 점수 주소값  을 받아 점수값 return

'''
import ctypes
import win32process


class MemoryRead:

    OpenProcess = ctypes.windll.kernel32.OpenProcess
    ReadProcessMemory = ctypes.windll.kernel32.ReadProcessMemory
    CloseHandle = ctypes.windll.kernel32.CloseHandle
    PROCESS_ALL_ACCESS = 0xFFF

    hwnd = None
    pid = None

    processHandle = None

    buffer = ctypes.c_char_p(b"0")
    bufferSize = len(buffer.value)
    bytesRead = ctypes.c_ulong(0)

    #arg1. 가지고올 기준 주소 arg2. 윈도우핸들러
    def __init__(self, address, _hwnd):
        self.address = address
        self.hwnd = _hwnd

        self.pid = self.getpid()

        self.processHandle = self.OpenProcess(self.PROCESS_ALL_ACCESS, False, self.pid)
    def getpid(self):
        tmp, _pid = win32process.GetWindowThreadProcessId(self.hwnd)
        return _pid

    def getvalue(self):

        is_start = False

        if self.ReadProcessMemory(self.processHandle, self.address, self.buffer, self.bufferSize, ctypes.byref(self.bytesRead)):
            #com_score = buffer.value
            com_score = int.from_bytes(self.buffer.value, byteorder='little')
            self.ReadProcessMemory(self.processHandle, self.address+0x04, self.buffer, self.bufferSize, ctypes.byref(self.bytesRead))
            #my_score = buffer.value
            my_score = int.from_bytes(self.buffer.value, byteorder='little')
            self.ReadProcessMemory(self.processHandle, self.address + 0x0C, self.buffer, self.bufferSize, ctypes.byref(self.bytesRead))
            # 진행상태FLAG 0:(최초게임시작)공떨어지기전 1: 공떨어지기전 2:게임중 3:공이 땅에 닿임 A: 메뉴
            #flag = buffer.value
            flag = int.from_bytes(self.buffer.value, byteorder='little')



            if flag != 10: #게임중일때만
                is_start = True


            return com_score, my_score, is_start, flag
        else:
            return -1

    def close(self):
        self.CloseHandle(self.processHandle)
