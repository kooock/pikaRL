'''
화면 캡쳐 후 RGB로 변환 numpy 형식으로 반환
'''

import win32gui
import win32ui
from ctypes import windll
from PIL import Image
import numpy as np
from PIL import ImageGrab


def rgb2gray(rgb):
    r, g, b = rgb[:, :, 0], rgb[:, :, 1], rgb[:, :, 2]
    gray = 0.2989 * r + 0.5870 * g + 0.1140 * b

    gray = np.uint8(gray)

    return gray

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
        win32gui.SetForegroundWindow(self.hwnd)
        bbox = win32gui.GetWindowRect(self.hwnd)

        img = ImageGrab.grab(bbox)
        img = img.resize([84, 84], Image.ANTIALIAS)
        pilim = np.array(img, dtype=np.uint8)
        pilim = rgb2gray(pilim)

        return pilim
