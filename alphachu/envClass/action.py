
'''
작업중.............
'''

class action :
    wsh = client.Dispatch('WScript.Shell')
    windowname = None

    #키를 누르고 때는 시간###
    interval_time = None
    #########################

    '''
    #키맵 정의 하기
    keyMap = {1: 
    2: 
    3: 
    4: 
    5: 
    ...}
    
    '''
    def __init__(self, _windowname, _interval_time):
        self.windowname = _windowname
        self.interval_time = _interval_time

    def sendKey(self, _keynum):

        wsh.AppActivate(self.windowname)

        '''
        win32api.keybd_event( 키맵 , 0, 0, 0)
        sleep(self.interval_time)
        win32api.keybd_event( 키맵 , 0, win32con.KEYEVENTF_KEYUP, 0)
        sleep(self.interval_time)
        '''
