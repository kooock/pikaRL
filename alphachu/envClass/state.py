'''
화면 캡쳐 후 RGB로 변환 numpy 형식으로 반환
'''

import win32gui
import win32ui
from ctypes import windll
from PIL import Image
import numpy as np


class state:
    hwnd = None

    def __init__(self, _hwnd):
        self.hwnd = _hwnd

    def getstate(self):
        left, top, right, bot = win32gui.GetWindowRect(self.hwnd)
        w = right - left
        h = bot - top

        hwndDC = win32gui.GetWindowDC(self.hwnd)
        mfcDC = win32ui.CreateDCFromHandle(hwndDC)
        saveDC = mfcDC.CreateCompatibleDC()

        saveBitMap = win32ui.CreateBitmap()
        saveBitMap.CreateCompatibleBitmap(mfcDC, w, h)

        saveDC.SelectObject(saveBitMap)

        result = windll.user32.PrintWindow(self.hwnd, saveDC.GetSafeHdc(), 0)

        bmpinfo = saveBitMap.GetInfo()
        bmpstr = saveBitMap.GetBitmapBits(True)

        # RGB로 변환
        im = Image.frombuffer(
            'RGB',
            (bmpinfo['bmWidth'], bmpinfo['bmHeight']),
            bmpstr, 'raw', 'BGRX', 0, 1)

        # np.set_printoptions(threshold=np.inf) #threshold 값 잘 조정하면 print로 numpy 전체 값 볼수있음

        pilim = np.array(im, dtype=np.uint8)

        pilim = np.transpose(pilim,axes=(1, 0, 2))
        return pilim