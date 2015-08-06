using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace VitPro.Net {

	public abstract class Server<T> where T: Message {

		static ILog log = LogManager.GetLogger(typeof(Server<T>));

        UdpClient udpServer;

        public Server(int port) {
            udpServer = new UdpClient(port);
            var thread = new Thread(() => Run(port));
            thread.IsBackground = true;
            thread.Start();
        }

        ConcurrentQueue<Tuple<T, IPEndPoint>> messages = new ConcurrentQueue<Tuple<T, IPEndPoint>>();

        HashSet<IPEndPoint> ips = new HashSet<IPEndPoint>();

		public void Broadcast(T message) {
			foreach (var ip in ips) {
				udpServer.SendMessage(message, ip);
			}
		}

        void Run(int port) {
            while (true) {
                var remoteEP = new IPEndPoint(IPAddress.Any, port);
                var message = udpServer.ReceiveMessage<T>(ref remoteEP);
                if (!ips.Contains(remoteEP)) {
					log.Info(string.Format("New client connected: {0}", remoteEP.Address));
                    ips.Add(remoteEP);
                }
                messages.Enqueue(Tuple.Create(message, remoteEP));
            }
        }

        public Tuple<long, long> GetTraffic() {
            return Tuple.Create(udpServer.GetTrafficSent(), udpServer.GetTrafficReceived());
        }

        public void Handle() {
            Tuple<T, IPEndPoint> message;
            while (messages.TryDequeue(out message)) {
				var replies = Handle(message.Item1);
				if (replies != null)
					foreach (var reply in replies)
						udpServer.SendMessage(reply, message.Item2);
            }
        }

		public void SendTo(T message, int who) {
			udpServer.SendMessage(message, who);
		}

		protected abstract IEnumerable<T> Handle(T message);

    }

}