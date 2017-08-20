import win32con
import win32gui
import threading
from . import MemoryRead
from . import state
from . import action

class env:

    keyMap = {
        0: win32con.VK_LEFT,
        1: win32con.VK_RIGHT,
        2: win32con.VK_UP,
        3: win32con.VK_DOWN,
        4: win32con.VK_RETURN
    }

    pixel_shape = [463, 363 ,3]

    action_space_num = 5

    interval_time = 0.01

    #argu : 피카츄배구 윈도우창 이름 ( PIKA ) ,
    def __init__(self, pika_windowname, _score_address):

        self.score_address = _score_address

        self.hwnd = win32gui.FindWindowEx(0, 0, None, pika_windowname)

        self.stateBuffer = 0

        self.comScoreBuffer = 0
        self.agentScoreBuffer = 0

        self.comScoreOld = 0
        self.agentScoreOld = 0

        self.isGaming = False

        self.isOpened = True

        self.inputAction = action.action(_windowname = pika_windowname,_interval_time = self.interval_time)

        self.memmoryReadThread = threading.Thread(target=self._asyncGetScoreValue)
        self.pixelReadThread = threading.Thread(target=self._asyncGetScoreValue)

        self.memmoryReadThread.start()
        self.pixelReadThread.start()


    def _asyncGetScoreValue(self):

        memoryReader = MemoryRead.MemoryRead(address=self.score_address, _hwnd=self.hwnd)

        while self.isOpened:
            self.comScoreBuffer, self.agentScoreBuffer, self.isGaming = memoryReader.getvalue()
    def _asyncGetState(self):

        stateReader = state.state(_hwnd=self.hwnd)

        while self.isOpened:
            if self.isGaming:
                self.stateBuffer = stateReader.getstate()
            else:
                self.stateBuffer = 0

    def computeReward(self):

        comScoreBuffer = self.comScoreBuffer
        agentScoreBuffer = self.agentScoreBuffer

        reward1 = 0
        reward2 = 0
        reward3 = 0


        if self.isGaming:
            reward1 = -15 * (comScoreBuffer - self.comScoreOld)
            reward2 = 15 * (agentScoreBuffer - self.agentScoreOld)

            #game finish
            if self.comScoreBuffer == 15:
                reward3 = -150
            if self.agentScoreBuffer == 15:
                reward3 = 150


            self.comScoreOld = comScoreBuffer
            self.agentScoreOld = agentScoreBuffer


        return reward1 + reward2 + reward3

    def step(self, _action):

        self.inputAction.sendKey(_action)

        if self.isGaming == False:
            done = True

        _state = self.stateBuffer
        reward = self.computeReward()


        return _state, reward, done

    def reset(self):
        #game reset action
        self.inputAction.game_reset()

        # var reset
        # ???

    def close(self):
        self.isOpened = False
        self.memmoryReadThread.join()
        self.pixelReadThread.join()
