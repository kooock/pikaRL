import tensorflow as tf
import numpy as np

class DQN:
    def __init__(self, session, input_x_size, input_y_size, input_channel, output_size, name="main"):
        self.session = session
        self.input_x_size = input_x_size
        self.input_y_size = input_y_size
        self.input_channel = input_channel
        self.output_size = output_size
        self.network_name = name
        self._buildNetwork()
        self.saver = tf.train.Saver()

    def _buildNetwork(self, last_layer_input_size=256, learning_rate=0.001):
        with tf.variable_scope(self.network_name):
            self._input = tf.placeholder(dtype=tf.float32,
                                         shape=[None, self.input_x_size, self.input_y_size, self.input_channel],
                                         name="input_feature")

            with tf.name_scope(self.network_name + "_Conv_set_layer1"):
                filter1 = tf.Variable(tf.random_normal([5, 5, 3, 32], stddev=0.01))
                self.conv_layer1 = tf.nn.relu(tf.nn.conv2d(self._input, filter1, strides=[1, 2, 2, 1], padding='SAME'))
                self.pooling_layer1 = tf.nn.max_pool(self.conv_layer1, ksize=[1, 2, 2, 1], strides=[1, 2, 2, 1], padding='SAME')
            print(self.conv_layer1)
            with tf.name_scope(self.network_name + "_Conv_set_layer2"):
                filter2 = tf.Variable(tf.random_normal([5, 5, 32, 64], stddev=0.01))
                self.conv_layer2 = tf.nn.relu(tf.nn.conv2d(self.pooling_layer1, filter2, strides=[1, 2, 2, 1], padding='SAME'))
                self.pooling_layer2 = tf.nn.max_pool(self.conv_layer2, ksize=[1, 2, 2, 1], strides=[1, 2, 2, 1], padding='SAME')
            with tf.name_scope(self.network_name + "_Conv_set_layer3"):
                filter3 = tf.Variable(tf.random_normal([5, 5, 64, 128], stddev=0.01))
                self.conv_layer3 = tf.nn.relu(tf.nn.conv2d(self.pooling_layer2, filter3, strides=[1, 2, 2, 1], padding='SAME'))
                self.pooling_layer3 = tf.nn.max_pool(self.conv_layer3, ksize=[1, 2, 2, 1], strides=[1, 2, 2, 1], padding='SAME')
                seq_pooling_layer3 = tf.contrib.layers.flatten(self.pooling_layer3)

            with tf.name_scope(self.network_name + 'FC_Layer1'):
                FC1_W = tf.get_variable("FC1_W", shape=[4*3*128, last_layer_input_size], initializer=tf.contrib.layers.xavier_initializer())
                #        FC_1 = tf. nn.tanh(tf.matmul(C4,FC1_W) + FC1_b)
                FC_1 = tf.nn.relu(tf.matmul(seq_pooling_layer3, FC1_W))

            with tf.name_scope(self.network_name + 'FC_Layer2'):
                FC2_W = tf.get_variable("FC2_W", shape=[last_layer_input_size, self.output_size], initializer=tf.contrib.layers.xavier_initializer())
                #        FC_1 = tf. nn.tanh(tf.matmul(C4,FC1_W) + FC1_b)
                self.q_predict = tf.matmul(FC_1, FC2_W)

        self._label = tf.placeholder(dtype=tf.float32,shape=[None,self.output_size])

        self._loss = tf.reduce_mean(tf.square(self._label - self.q_predict))

        self._train = tf.train.AdamOptimizer(learning_rate=learning_rate).minimize(self._loss)

    def predict(self, state):
        feed_state = np.reshape(state,[1,self.input_x_size,self.input_y_size,self.input_channel])
        q = self.session.run(self.q_predict, feed_dict={self._input: feed_state})

        return q

    def update(self, input_stack, label_stack):

        loss, _ = self.session.run([self._loss, self._train], feed_dict={self._input: input_stack,
                                                               self._label: label_stack})
        save_path = self.saver.save(self.session, "./save/predict_model", global_step=0)

        print(loss)





