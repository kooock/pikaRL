import numpy as np
import tensorflow as tf
import random
import dqn
import threading
from collections import deque
import signal
import sys
import os
import time

from PIL import Image
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))
from envClass import env

import logging
import logging.handlers

class Agent:
    def __init__(self, window_name, score_address):
        self.env = env.env(window_name,score_address)

        self.input_y_size = self.env.pixel_shape[0]
        self.input_x_size = self.env.pixel_shape[1]
        self.input_channel = self.env.pixel_shape[2]
        self.output_size = self.env.action_space_num

        self.max_episodes = 2000

        self.dis = 0.99
        self.REPLAY_MEMORY = 1000000
        self.TRAIN_START_MEMORY = 50000
        self.replay_buffer = deque()
        self.sess = tf.Session()
        self.is_train = False
        self.step = 0


    def async_training(self, copy_ops, main_dqn, target_dqn):
        print("is_train: " + str(self.is_train))
        while self.is_train:
            if len(self.replay_buffer) >= self.TRAIN_START_MEMORY and self.step % 4 == 0:
                #update fequency is 4
                mini_batch = random.sample(self.replay_buffer, 32)
                loss = self.replay_train(main_dqn, target_dqn, mini_batch)
                print("loss: " + str(loss))
                if self.step == 10000:
                    #tartget updata frequency is 10000
                    self.sess.run(copy_ops)
            time.sleep(0.1)

    def replay_train(self,main_dqn,target_dqn, train_batch):
        input_array = []
        label_array = []
        for state, action, reward, next_state, done in train_batch:
            Q = main_dqn.predict(state)

            print("Q")
            print(Q)
            if done:
                Q[0, action] = reward
            else:
                Q[0, action] = reward + self.dis * np.max(target_dqn.predict(next_state))

            print("reward : " + str(reward) + "\tQ [action] : " +str(Q[0, action]))

            label_array.append(Q[0])
            input_array.append(state)

        input_stack = np.stack(input_array)
        label_stack = np.stack(label_array)

        return main_dqn.update(input_stack,label_stack)

    def get_copy_var_ops(self,dest_scope_name="target", src_scope_name="main"):

        op_holder = []

        src_vars = tf.get_collection(
            tf.GraphKeys.TRAINABLE_VARIABLES, scope=src_scope_name)
        dest_vars = tf.get_collection(
            tf.GraphKeys.TRAINABLE_VARIABLES, scope=dest_scope_name)

        for src_var, dest_var in zip(src_vars,dest_vars):
            op_holder.append(dest_var.assign(src_var.value()))

        return op_holder

    def play_agent(self,main_dqn):

        s = self.env.reset()

        reward_sum = 0

        while True:
            a = np.argmax(main_dqn.predict(s))
            s, reward, done, _ = self.env.step(a)
            reward_sum += reward

            if done:
                print("Total score: " + str(reward_sum))
                break

    def train(self):
        self.is_train = True


        main_dqn = dqn.DQN(self.sess,self.input_x_size,self.input_y_size,self.input_channel,self.output_size,name="main")


        saver = tf.train.Saver()

        if os.path.exists("./save/checkpoint"):
            saver.restore(sess=self.sess, save_path="save/predict_model")
        else:
            tf.global_variables_initializer().run(session=self.sess)

        target_dqn = dqn.DQN(self.sess, self.input_x_size, self.input_y_size, self.input_channel, self.output_size,
                             name="target")

        copy_ops = self.get_copy_var_ops(dest_scope_name="target",src_scope_name="main")

        self.sess.run(copy_ops)

        training_thread = threading.Thread(target=self.async_training, args=(copy_ops,main_dqn,target_dqn))
        print("Training Thread Start")
        training_thread.start()

        for episode in range(self.max_episodes):
            e = 1./((episode /10) +1)
            done = False
            step_count = 0
            state = self.env.reset()

            while not done:
                if np.random.rand(1) < e:
                    action = self.env.randomAction()
                else:
                    q_predict = main_dqn.predict(state)
                    action = np.argmax(q_predict)
                    print("Q predict : " + str(q_predict))
                    print("predict action : " + str(action))
                next_state, reward, done, info = self.env.step(action)

                #print("action : " + str(action) + "\t reword : " + str(reward))

                if info == 2:
                    self.replay_buffer.append((state, action, reward, next_state, done))
                if reward != 0:
                    self.replay_buffer.append((state, action, reward, next_state, True))

                if len(self.replay_buffer) > self.REPLAY_MEMORY:
                    self.replay_buffer.popleft()

                state = next_state

                step_count += 1
                self.step += 1

                if step_count > 10000:
                    break

            print("episode: ", episode, " steps: ", step_count)
            # sync training
            '''
            if episode % 10 == 1:
            print("start training")
            for _ in range(50):
                minibatch = random.sample(self.replay_buffer, 100)
                loss, _ = self.replay_train(mainDQN,targetDQN,minibatch)
            print("loss: ", loss)
            self.sess.run(copy_ops)
            print("finish training")
            '''

        self.is_train = False
        training_thread.join()
        return main_dqn



if __name__ == "__main__":
    args = sys.argv
    if len(args) < 3:
        print("arguments is not enough")
        exit()
    else:
        pika_windowname = args[1]
        score_address = args[2]
        score_address = int(score_address, 16)

    agent = Agent(pika_windowname,score_address)

    def signal_handler(signal, frame):
        agent.env.close()
        sys.exit(0)

    signal.signal(signal.SIGINT, signal_handler)
    main_dqn = agent.train()
    agent.play_agent(main_dqn)