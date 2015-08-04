using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace VitPro.Net {

    public class Server<T> {

		static ILog log = LogManager.GetLogger(typeof(Server<T>));

        UdpClient udpServer;
		T model;

        public Server(T model, int port) {
			this.model = model;
            udpServer = new UdpClient(port);
            var thread = new Thread(() => Run(port));
            thread.IsBackground = true;
            thread.Start();
        }

        ConcurrentQueue<Tuple<Message<T>, IPEndPoint>> messages = new ConcurrentQueue<Tuple<Message<T>, IPEndPoint>>();

        HashSet<IPEndPoint> ips = new HashSet<IPEndPoint>();

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
            Tuple<Message<T>, IPEndPoint> message;
            while (messages.TryDequeue(out message)) {
                var reply = message.Item1.Handle(model);
				if (reply != null) 
					udpServer.SendMessage(reply, message.Item2);
            }
        }

    }

}