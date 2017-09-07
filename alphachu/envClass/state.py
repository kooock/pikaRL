'''
화면 캡쳐 후 RGB로 변환 numpy 형식으로 반환
'''

import win32gui
import win32ui
from ctypes import windll
from PIL import Image
import numpy as np
from PIL import ImageGrab


class state:
    hwnd = None

    def __init__(self, _hwnd):
        self.hwnd = _hwnd

        left, top, right, bot = win32gui.GetWindowRect(self.hwnd)
        self.w = right - left
        self.h = bot - top

        self.hwndDC = win32gui.GetWindowDC(self.hwnd)
        self.mfcDC = win32ui.CreateDCFromHandle(self.hwndDC)
        self.saveDC = self.mfcDC.CreateCompatibleDC()

    def getstate(self):
        #saveDC = self.mfcDC.CreateCompatibleDC()
        saveBitMap = win32ui.CreateBitmap()
        saveBitMap.CreateCompatibleBitmap(self.mfcDC, self.w, self.h)

        self.saveDC.SelectObject(saveBitMap)

        #result = windll.user32.PrintWindow(self.hwnd, saveDC.GetSafeHdc(), 0)

        bmpinfo = saveBitMap.GetInfo()
        bmpstr = saveBitMap.GetBitmapBits(True)

        # RGB로 변환
        im = Image.frombuffer(
            'RGB',
            (bmpinfo['bmWidth'], bmpinfo['bmHeight']),
            bmpstr, 'raw', 'BGRX', 0, 1)

        # np.set_printoptions(threshold=np.inf) #threshold 값 잘 조정하면 print로 numpy 전체 값 볼수있음

        pilim = np.array(im, dtype=np.uint8)
        #win32gui.DeleteObject(saveBitMap)
        #win32gui.DeleteDC(saveDC)

        return pilim

    def getstateTest(self):
        win32gui.SetForegroundWindow(self.hwnd)
        bbox = win32gui.GetWindowRect(self.hwnd)
        img = ImageGrab.grab(bbox)
        pilim = np.array(img, dtype=np.uint8)
        return pilim