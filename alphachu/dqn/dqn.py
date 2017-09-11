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

    def lrelu(self,x, alpha):
        return tf.nn.relu(x) - alpha * tf.nn.relu(-x)

    def _buildNetwork(self, last_layer_input_size=512, learning_rate=0.00025):
        with tf.variable_scope(self.network_name):
            self.dropout_late = tf.placeholder(tf.float32)
            self._input = tf.placeholder(dtype=tf.float32,
                                         shape=[None, self.input_y_size, self.input_x_size, self.input_channel],
                                         name="input_feature")

            with tf.name_scope(self.network_name + "_Conv_set_layer1"):
                filter1 = tf.get_variable("Conv1_W", shape=[8, 8, 4, 32],initializer=tf.contrib.layers.xavier_initializer())
                self.conv_layer1 = tf.nn.relu(tf.nn.conv2d(self._input, filter1, strides=[1, 4, 4, 1], padding='VALID'))
                self.conv_layer1 = tf.nn.dropout(self.conv_layer1,self.dropout_late)
            print(self.conv_layer1)
            with tf.name_scope(self.network_name + "_Conv_set_layer2"):
                filter2 = tf.get_variable("Conv2_W", shape=[4, 4, 32, 64],initializer=tf.contrib.layers.xavier_initializer())
                self.conv_layer2 = tf.nn.relu(tf.nn.conv2d(self.conv_layer1, filter2, strides=[1, 2, 2, 1], padding='VALID'))
                self.conv_layer2 = tf.nn.dropout(self.conv_layer2, self.dropout_late)
            with tf.name_scope(self.network_name + "_Conv_set_layer3"):
                filter3 = tf.get_variable("Conv3_W", shape=[3, 3, 64, 64],initializer=tf.contrib.layers.xavier_initializer())
                self.conv_layer3 = tf.nn.relu(tf.nn.conv2d(self.conv_layer2, filter3, strides=[1, 1, 1, 1], padding='VALID'))
                self.seq_layer3= tf.contrib.layers.flatten(self.conv_layer3)

#Hack : fc!_W SIZE IS 6144
            with tf.name_scope(self.network_name + 'FC_Layer1'):
                FC1_W = tf.get_variable("FC1_W", shape=[7*7*64, last_layer_input_size], initializer=tf.contrib.layers.xavier_initializer())
                FC1_b = tf.get_variable("FC1_b", shape=[1,last_layer_input_size], initializer=tf.contrib.layers.xavier_initializer())
                #        FC_1 = tf. nn.tanh(tf.matmul(C4,FC1_W) + FC1_b)
                FC_1 = tf.nn.relu(tf.matmul(self.seq_layer3, FC1_W) + FC1_b)
                FC_1 = tf.nn.dropout(FC_1,self.dropout_late)

            with tf.name_scope(self.network_name + 'FC_Layer2'):
                FC2_W = tf.get_variable("FC2_W", shape=[last_layer_input_size, self.output_size], initializer=tf.contrib.layers.xavier_initializer())
                #        FC_1 = tf. nn.tanh(tf.matmul(C4,FC1_W) + FC1_b)
                FC2_b = tf.get_variable("FC2_b", shape=[1, self.output_size],
                                        initializer=tf.contrib.layers.xavier_initializer())
                self.q_predict = tf.matmul(FC_1, FC2_W) + FC2_b

        self._label = tf.placeholder(dtype=tf.float32,shape=[None,self.output_size])

        self._loss = tf.reduce_mean(tf.square(self._label-self.q_predict))

        self._train = tf.train.RMSPropOptimizer(learning_rate, decay=.95, epsilon=.01).minimize(self._loss)

    def predict(self, state):
        feed_state = np.reshape(state,[1,self.input_y_size,self.input_x_size,self.input_channel])
        q = self.session.run(self.q_predict, feed_dict={self._input: feed_state, self.dropout_late: 1.0})
        #print("predict seq :" + str(s4))
        return q

    def update(self, input_stack, label_stack):
        loss, _= self.session.run([self._loss, self._train], feed_dict={self._input: input_stack,
                                                               self._label: label_stack, self.dropout_late: 0.8})
        save_path = self.saver.save(self.session, "save/predict_model")

        return loss




