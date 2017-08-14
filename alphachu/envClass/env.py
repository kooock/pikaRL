import win32con
import win32gui

class env:

    hwnd = None
    score_address = None

    keyMap = {
        0: win32con.VK_LEFT,
        1: win32con.VK_RIGHT,
        2: win32con.VK_UP,
        3: win32con.VK_RETURN
    }

    #argu : 피카츄배구 윈도우창 이름 ( PIKA ) ,
    def __init__(self, pika_windowname, _score_address):

        self.score_address = _score_address

        self.hwnd = win32gui.FindWindowEx(0, 0, None, pika_windowname)

'''
    def step(self, action):
        return None
'''