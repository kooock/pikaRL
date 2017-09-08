import numpy as np
import sys
import os
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))
from envClass import env
import time
from PIL import Image
import scipy.misc as ms



def rgb2gray(rgb):
    r, g, b = rgb[:, :, 0], rgb[:, :, 1], rgb[:, :, 2]
    gray = 0.2989 * r + 0.5870 * g + 0.1140 * b

    gray = np.uint8(gray)

    return gray

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

    state, _, _ ,_ = env.step(0)
    print(np.shape(state))

    env.close()





