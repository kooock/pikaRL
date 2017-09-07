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

    def _buildNetwork(self, last_layer_input_size=64, learning_rate=0.0001):
        with tf.variable_scope(self.network_name):
            self.dropout_late = tf.placeholder(tf.float32)
            self._input = tf.placeholder(dtype=tf.float32,
                                         shape=[None, self.input_y_size, self.input_x_size, self.input_channel],
                                         name="input_feature")

            self.resize_input = tf.image.resize_images(self._input,[290,370])

            with tf.name_scope(self.network_name + "_Conv_set_layer1"):
                filter1 = tf.Variable(tf.random_normal([5, 5, 3, 32], stddev=0.01))
                self.conv_layer1 = tf.nn.relu(tf.nn.conv2d(self.resize_input, filter1, strides=[1, 3, 3, 1], padding='SAME'))
                self.pooling_layer1 = tf.nn.max_pool(self.conv_layer1, ksize=[1, 2, 2, 1], strides=[1, 2, 2, 1], padding='SAME')
                self.pooling_layer1 = tf.nn.dropout(self.pooling_layer1,self.dropout_late)
            print(self.conv_layer1)
            with tf.name_scope(self.network_name + "_Conv_set_layer2"):
                filter2 = tf.Variable(tf.random_normal([5, 5, 32, 32], stddev=0.01))
                self.conv_layer2 = tf.nn.relu(tf.nn.conv2d(self.pooling_layer1, filter2, strides=[1, 3, 3, 1], padding='SAME'))
                self.pooling_layer2 = tf.nn.max_pool(self.conv_layer2, ksize=[1, 2, 2, 1], strides=[1, 2, 2, 1], padding='SAME')
                self.pooling_layer2 = tf.nn.dropout(self.pooling_layer2, self.dropout_late)
            with tf.name_scope(self.network_name + "_Conv_set_layer3"):
                filter3 = tf.Variable(tf.random_normal([4, 4, 32, 64], stddev=0.01))
                self.conv_layer3 = tf.nn.relu(tf.nn.conv2d(self.pooling_layer2, filter3, strides=[1, 2, 2, 1], padding='SAME'))
                self.pooling_layer3 = tf.nn.max_pool(self.conv_layer3, ksize=[1, 2, 2, 1], strides=[1, 2, 2, 1], padding='SAME')
                self.pooling_layer3 = tf.nn.dropout(self.pooling_layer3, self.dropout_late)
            with tf.name_scope(self.network_name + "_Conv_set_layer4"):
                filter4 = tf.Variable(tf.random_normal([3, 3, 64, 64], stddev=0.01))
                self.conv_layer4 = tf.nn.relu(tf.nn.conv2d(self.pooling_layer3, filter4, strides=[1, 2, 2, 1], padding='SAME'))
                self.conv_layer4 = tf.nn.dropout(self.conv_layer4, self.dropout_late)
                #self.pooling_layer4 = tf.nn.max_pool(self.conv_layer4, ksize=[1, 2, 2, 1], strides=[1, 2, 2, 1], padding='SAME')
                self.seq_layer4= tf.contrib.layers.flatten(self.conv_layer4)

#Hack : fc!_W SIZE IS 6144
            with tf.name_scope(self.network_name + 'FC_Layer1'):
                FC1_W = tf.get_variable("FC1_W", shape=[256, last_layer_input_size], initializer=tf.contrib.layers.xavier_initializer())
                FC1_b = tf.get_variable("FC1_b", shape=[1,last_layer_input_size], initializer=tf.contrib.layers.xavier_initializer())
                #        FC_1 = tf. nn.tanh(tf.matmul(C4,FC1_W) + FC1_b)
                FC_1 = tf.nn.relu(tf.matmul(self.seq_layer4, FC1_W) + FC1_b)
                FC_1 = tf.nn.dropout(FC_1,self.dropout_late)

            with tf.name_scope(self.network_name + 'FC_Layer2'):
                FC2_W = tf.get_variable("FC2_W", shape=[last_layer_input_size, self.output_size], initializer=tf.contrib.layers.xavier_initializer())
                #        FC_1 = tf. nn.tanh(tf.matmul(C4,FC1_W) + FC1_b)
                FC2_b = tf.get_variable("FC2_b", shape=[1, 5],
                                        initializer=tf.contrib.layers.xavier_initializer())
                self.q_predict = tf.matmul(FC_1, FC2_W) + FC2_b

        self._label = tf.placeholder(dtype=tf.float32,shape=[None,self.output_size])

        self._loss = tf.reduce_mean(tf.nn.softmax_cross_entropy_with_logits(logits=self.q_predict,labels=self._label))

        self._train = tf.train.AdamOptimizer(learning_rate=learning_rate).minimize(self._loss)

    def predict(self, state):
        feed_state = np.reshape(state,[1,self.input_y_size,self.input_x_size,self.input_channel])
        q,s4 = self.session.run([self.q_predict, self.seq_layer4], feed_dict={self._input: feed_state, self.dropout_late: 1.0})
        #print("predict seq :" + str(s4))
        return q

    def update(self, input_stack, label_stack):
        loss, _= self.session.run([self._loss, self._train], feed_dict={self._input: input_stack,
                                                               self._label: label_stack, self.dropout_late: 0.7})
        save_path = self.saver.save(self.session, "save/predict_model")

        return loss




