using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace VitPro.Net {

	public abstract class Client<T> where T : Message {

		static ILog log = LogManager.GetLogger(typeof(Client<T>));

        UdpClient udpClient;
        IPEndPoint ep;

        volatile bool finished = false;

        public Client(string address, int port) {
			log.Info(string.Format("Trying to connect to {0}:{1}", address, port));
            udpClient = new UdpClient();
            ep = new IPEndPoint(IPAddress.Parse(address), port);
            udpClient.Connect(ep);
            var thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();
        }

        public virtual void Stop() {
            udpClient.Close();
            finished = true;
        }

        ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

        volatile Exception disconnectedReason;

        void Run() {
            try {
                while (!finished) {
                    queue.Enqueue(udpClient.ReceiveMessage<T>(ref ep));
                }
            } catch (SocketException e) {
                disconnectedReason = e;
            }
        }

        public Tuple<long, long> GetTraffic() {
            return Tuple.Create(udpClient.GetTrafficSent(), udpClient.GetTrafficReceived());
        }

        public void Send(T message) {
            try {
                udpClient.SendMessage(message);
            } catch (SocketException e) {
                disconnectedReason = e;
            }
        }

        public void Handle() {
            if (disconnectedReason != null) {
                throw new NetException("Disconnected", disconnectedReason);
            }
            T message;
            while (queue.TryDequeue(out message)) {
				var replies = Handle(message);
				if (replies != null)
					foreach (var reply in replies)
                    	udpClient.SendMessage(reply);
            }
        }

		protected abstract IEnumerable<T> Handle(T message);

    }

}