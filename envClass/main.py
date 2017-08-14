
import MemoryRead
import env
import state

name = "PIKA"
address = 0x00660A14

ev = env.env(name, address) # 피카츄배구 프로세스에 대한 환경설정?


mr = MemoryRead.MemoryRead(address, ev.hwnd)

print(mr.getvalue()) #점수 출력

state = state.state(ev.hwnd).getstate()

#print(state) #픽셀값 출력 numpy형