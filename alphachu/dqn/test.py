import numpy
import sys
import os
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))
from envClass import env
import time
from PIL import Image


if __name__ == "__main__":
    args = sys.argv
    if len(args) < 3:
        print("arguments is not enough")
        exit()
    else:
        pika_windowname = args[1]
        score_address = args[2]
        score_address = int(score_address, 16)

    env = env.env(pika_windowname,score_address)
    env.reset()
    time.sleep(2)

    for i in range(10):
        state, _, _ ,_ = env.step(0)
        img = Image.fromarray(state, 'RGB')
        img.save('C:/Users/koock/Pictures/my-' + str(i) + '.png')

        time.sleep(10)





