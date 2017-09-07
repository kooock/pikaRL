import win32con
import win32gui
import threading
from . import MemoryRead
from . import state
from . import action
import random
import time

class env:

    keyMap = {
        0: win32con.VK_LEFT,
        1: win32con.VK_RIGHT,
        2: win32con.VK_UP,
        3: win32con.VK_DOWN,
        4: win32con.VK_RETURN
    }


    action_space_num = 5

    interval_time = 0.01

    #argu : 피카츄배구 윈도우창 이름 ( PIKA ) ,
    def __init__(self, pika_windowname, _score_address):

        self.score_address = _score_address

        self.hwnd = win32gui.FindWindowEx(0, 0, None, pika_windowname)

        self.pixel_shape = self.checkStateShape()
        self.stateBuffer = self.stateInit()

        self.comScoreBuffer = 0
        self.agentScoreBuffer = 0

        self.comScoreOld = 0
        self.agentScoreOld = 0

        self.isGaming = False

        self.flag = 10

        self.isOpened = True
        self.isFinished = True

        self.inputAction = action.action(_windowname = pika_windowname,_interval_time = self.interval_time)

        self.memmoryReadThread = threading.Thread(target=self._asyncGetScoreValue)
        self.pixelReadThread = threading.Thread(target=self._asyncGetState)

        self.memmoryReadThread.start()
        self.pixelReadThread.start()


    def checkStateShape(self):
        stateReader = state.state(_hwnd=self.hwnd)
        stateBuffer = stateReader.getstateTest()
        return stateBuffer.shape

    def randomAction(self):
        randomKey = random.randint(0,4)
        return randomKey

    def stateInit(self):
        stateReader = state.state(_hwnd=self.hwnd)
        return stateReader.getstateTest()

    def _asyncGetScoreValue(self):

        memoryReader = MemoryRead.MemoryRead(address=self.score_address, _hwnd=self.hwnd)

        while self.isOpened:
            if self.isFinished:
                self.isGaming = False
            else:
                self.comScoreBuffer, self.agentScoreBuffer, self.isGaming, self.flag = memoryReader.getvalue()
            time.sleep(0.01)




    def _asyncGetState(self):

        stateReader = state.state(_hwnd=self.hwnd)

        while self.isOpened:
            if self.isGaming:
                self.stateBuffer = stateReader.getstateTest()
            time.sleep(0.01)



    def computeReward(self):

        comScoreBuffer = self.comScoreBuffer
        agentScoreBuffer = self.agentScoreBuffer

        reward1 = 0
        reward2 = 0
        reward3 = 0


        if self.isGaming:
            reward1 = -1 * (comScoreBuffer - self.comScoreOld)
            reward2 = 1 * (agentScoreBuffer - self.agentScoreOld)

            #game finish
            if self.comScoreBuffer == 15:
                reward3 = -100
                self.isFinished = True
            if self.agentScoreBuffer == 15:
                reward3 = 100
                self.isFinished = True


            self.comScoreOld = comScoreBuffer
            self.agentScoreOld = agentScoreBuffer


        return reward1 + reward2 + reward3



    def step(self, _action):

        self.inputAction.sendKey(_action)

        done = not self.isGaming

        info = self.flag

        _state = self.stateBuffer

        reward = self.computeReward()



        return _state, reward, done, info

    def reset(self):
        #game reset action
        self.inputAction.game_reset()
        self.isFinished = False
        state = self.stateBuffer
        return state
        # var reset
        # ???

    def close(self):
        self.isOpened = False
        self.memmoryReadThread.join()
        self.pixelReadThread.join()


