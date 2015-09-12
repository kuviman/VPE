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

        volatile bool finished = false;

        public Server(int port) {
            udpServer = new UdpClient(port);
            var thread = new Thread(() => Run(port));
            thread.IsBackground = true;
            thread.Start();
            log.Info("Server started");
        }

        public virtual void Stop() {
            udpServer.Close();
            finished = true;
            log.Info("Server stopped");
        }

        ConcurrentQueue<Tuple<T, IPEndPoint>> messages = new ConcurrentQueue<Tuple<T, IPEndPoint>>();

        ConcurrentDictionary<IPEndPoint, long> clients = new ConcurrentDictionary<IPEndPoint,long>();

        public virtual void Disconnect(int who) { }

		public void Broadcast(T message) {
            List<IPEndPoint> deadClients = new List<IPEndPoint>();
            foreach (var entry in clients) {
                if (System.Diagnostics.Stopwatch.GetTimestamp() - entry.Value > 1 * System.Diagnostics.Stopwatch.Frequency) {
                    long tmp;
                    log.Info(string.Format("Client disconnected: {0}", entry.Key));
                    if (clients.TryRemove(entry.Key, out tmp))
                        Disconnect(entry.Key.GetHashCode());
                }
            }
			foreach (var ip in clients.Keys) {
                try {
                    udpServer.SendMessage(message, ip);
                } catch (SocketException e) {
                    log.Warn(string.Format("Got exception while broadcasting to {0}", ip), e);
                }
			}
		}

        void Run(int port) {
            while (!finished) {
                try {
                    var remoteEP = new IPEndPoint(IPAddress.Any, port);
                    var message = udpServer.ReceiveMessage<T>(ref remoteEP);
                    if (!clients.ContainsKey(remoteEP)) {
                        log.Info(string.Format("New client connected: {0}", remoteEP.Address));
                    }
                    clients[remoteEP] = System.Diagnostics.Stopwatch.GetTimestamp();
                    messages.Enqueue(Tuple.Create(message, remoteEP));
                } catch (SocketException e) {
                    //log.Warn("Got exception while receiving message", e);
                }
            }
        }

        public Tuple<long, long> GetTraffic() {
            return Tuple.Create(udpServer.GetTrafficSent(), udpServer.GetTrafficReceived());
        }

        public void Handle() {
            try {
                Tuple<T, IPEndPoint> message;
                while (messages.TryDequeue(out message)) {
                    var replies = Handle(message.Item1);
                    if (replies != null)
                        foreach (var reply in replies)
                            udpServer.SendMessage(reply, message.Item2);
                }
            } catch (SocketException e) {
                log.Warn("Got exception while handling messages", e);
            }
        }

		public void SendTo(T message, int who) {
            try {
                udpServer.SendMessage(message, who);
            } catch (SocketException e) {
                log.Warn(string.Format("Got exception while sending message to {0}", who), e);
            }
		}

		protected abstract IEnumerable<T> Handle(T message);

    }

}