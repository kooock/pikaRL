import numpy as np
import tensorflow as tf
import random
import dqn
import threading
from collections import deque
from ..envClass import env
import signal
import sys

class Agent:
    def __init__(self, window_name, score_address):
        self.env = env(window_name,score_address)

        self.input_x_size = self.env.pixel_shape[0]
        self.input_y_size = self.env.pixel_shape[1]
        self.input_channel = self.env.pixel_shape[2]
        self.output_size = self.env.action_space_num

        self.max_episodes = 2000

        self.dis = 0.9
        self.REPLAY_MEMORY = 50000
        self.episode = 0
        self.replay_buffer = deque()
        self.sess = tf.Session()
        self.is_train = False

    def async_training(self, copy_ops, main_dqn, target_dqn):
        while self.is_train:
            if self.episode % 10 == 9:
                print("start training")
                for _ in range(50):
                    mini_batch = random.sample(self.replay_buffer, 50)
                    loss, _ = self.replay_train(main_dqn,target_dqn,mini_batch)
                print("loss: ", loss)
                self.sess.run(copy_ops)
                print("finish a unit training")



    def replay_train(self,main_dqn,target_dqn, train_batch):
        input_array = []
        label_stack = np.empty(0).reshape(0, self.output_size)

        for state, action, reward, next_state, done in train_batch:
            Q = main_dqn.predict(state)

            if done:
                Q[0, action] = reward
            else:
                Q[0, action] = reward + self.dis * np.max(target_dqn.predict(next_state))


            label_stack = np.vstack([label_stack,Q])
            input_array.append(state)

        input_stack = np.stack(input_array)
        main_dqn.update(input_stack,label_stack)

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
                print("Total score: ", reward_sum)
                break

    def train(self):
        self.is_train = True


        main_dqn = dqn.DQN(self.sess,self.input_x_size,self.input_y_size,self.input_channel,self.output_size,name="main")
        target_dqn = dqn.DQN(self.sess,self.input_x_size,self.input_y_size,self.input_channel,self.output_size,name="target")

        tf.global_variables_initializer().run()

        copy_ops = self.get_copy_var_ops(dest_scope_name="target",src_scope_name="main")

        self.sess.run(copy_ops)

        training_thread = threading.Thread(target=self.async_training, args=(copy_ops,main_dqn,target_dqn))
        training_thread.start()

        for episode in range(self.max_episodes):
            e = ((1 - ((episode + 1)/self.max_episodes)) * 0.8) + 0.2
            done = False
            step_count = 0
            state = self.env.reset()

            while not done:
                if np.random.rand(1) < e:
                    action = self.env.action_space.sample()
                else:
                    action = np.argmax(main_dqn.predict(state))
                next_state, reward, done, _ = self.env.step(action)

                self.replay_buffer.append((state, action, reward, next_state, done))

                if len(self.replay_buffer) > self.REPLAY_MEMORY:
                    self.replay_buffer.popleft()

                state = next_state

                step_count += 1

            print("episode: ", episode, " steps: ", step_count)
            self.episode = episode
#sync training
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

    agent = Agent(pika_windowname,score_address)

    def signal_handler(signal, frame):
        agent.env.close()
        sys.exit(0)

    signal.signal(signal.SIGINT, agent.signal_handler)
    main_dqn = agent.train()
    agent.play_agent(main_dqn)