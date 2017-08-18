import win32gui
from . import MemoryRead
from . import state
from . import action

class env:

    hwnd = None
    score_address = None



    #argu : 피카츄배구 윈도우창 이름 ( PIKA ) ,
    def __init__(self, pika_windowname, _score_address):

        self.score_address = _score_address

        self.hwnd = win32gui.FindWindowEx(0, 0, None, pika_windowname)


    def step(self, action):

        # com_score, my_score
        # 진행상태FLAG 0:(최초게임시작)공떨어지기전 1: 공떨어지기전 2:게임중 3:공이 땅에 닿임 A: 메뉴
        com_score, my_score, cur_flag = MemoryRead.MemoryRead(self.score_address, self.hwnd)

        #pixel
        cur_state = state.state(self.hwnd).getstate()



        return cur_state, reward, done

    def reset(self):


