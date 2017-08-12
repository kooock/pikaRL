import numpy as np
import tensorflow as tf
import random
import dqn
from collections import deque

import gym


env = gym.make("Tennis-v0")

input_x_size = env.observation_space.shape[0]
input_y_size = env.observation_space.shape[1]
input_channel = env.observation_space.shape[2]
output_size = env.action_space.n

dis = 0.9
REPLAY_MEMORY = 30000


def replay_train(mainDQN,targetDQN, train_batch):
    input_array = []
    label_stack = np.empty(0).reshape(0, output_size)

    for state, action, reward, next_state, done in train_batch:
        Q = mainDQN.predict(state)

        if done:
            Q[0, action] = reward
        else:
            Q[0, action] = reward + dis * np.max(targetDQN.predict(next_state))


        label_stack = np.vstack([label_stack,Q])
        input_array.append(state)

    input_stack = np.stack(input_array)
    return mainDQN.update(input_stack,label_stack)

def get_copy_var_ops(*,dest_scope_name="target", src_scope_name="main"):

    op_holder = []

    src_vars = tf.get_collection(
        tf.GraphKeys.TRAINABLE_VARIABLES, scope=src_scope_name)
    dest_vars = tf.get_collection(
        tf.GraphKeys.TRAINABLE_VARIABLES, scope=dest_scope_name)

    for src_var, dest_var in zip(src_vars,dest_vars):
        op_holder.append(dest_var.assign(src_var.value()))

    return op_holder

def play_agent(mainDQN):

    s = env.reset()

    reward_sum = 0

    while True:
        env.render()
        a = np.argmax(mainDQN.predict(s))
        s, reward, done, _ = env.step(a)
        reward_sum += reward

        if done:
            print("Total score: ", reward_sum)
            break

def main():
    max_episodes = 2000

    replay_buffer = deque()

    with tf.Session() as sess:
        mainDQN = dqn.DQN(sess,input_x_size,input_y_size,input_channel,output_size,name="main")
        targetDQN = dqn.DQN(sess,input_x_size,input_y_size,input_channel,output_size,name="target")

        tf.global_variables_initializer().run()

        copy_ops = get_copy_var_ops(dest_scope_name="target",
                                    src_scope_name="main")

        sess.run(copy_ops)



        for episode in range(max_episodes):
            e = ((1 - ((episode + 1)/max_episodes)) * 0.8) + 0.2
            done = False
            step_count = 0
            state = env.reset()

            while not done:
                env.render()
                if np.random.rand(1) < e:
                    action = env.action_space.sample()
                else:
                    action = np.argmax(mainDQN.predict(state))
                next_state, reward, done, _ = env.step(action)

                replay_buffer.append((state, action, reward, next_state, done))

                if len(replay_buffer) > REPLAY_MEMORY:
                    replay_buffer.popleft()

                state = next_state

                step_count += 1

            print("episode: ", episode, " steps: ", step_count)

            if episode % 10 == 1:
                print("start training")
                for _ in range(50):
                    minibatch = random.sample(replay_buffer, 100)
                    loss, _ = replay_train(mainDQN,targetDQN,minibatch)
                    print("loss: ", loss)
                    sess.run(copy_ops)
                print("finish training")
        play_agent(mainDQN)

if __name__ == "__main__":
    main()