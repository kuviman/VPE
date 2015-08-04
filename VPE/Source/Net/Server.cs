using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace VitPro.Net {

    public static class Server<T> {

        static UdpClient udpServer;

        public static void Start(int port) {
            udpServer = new UdpClient(port);
            var thread = new Thread(() => Run(port));
            thread.IsBackground = true;
            thread.Start();
            var thread2 = new Thread(RunReplier);
            thread2.IsBackground = true;
            thread2.Start();
        }

        static ConcurrentQueue<Tuple<Message<T>, IPEndPoint>> messages = new ConcurrentQueue<Tuple<Message<T>, IPEndPoint>>();
        static BlockingCollection<Tuple<Message<T>, IPEndPoint>> replies = new BlockingCollection<Tuple<Message<T>,IPEndPoint>>();

        static HashSet<IPEndPoint> ips = new HashSet<IPEndPoint>();

        static void Run(int port) {
            while (true) {
                var remoteEP = new IPEndPoint(IPAddress.Any, port);
                var message = udpServer.ReceiveMessage<T>(ref remoteEP);
                if (!ips.Contains(remoteEP)) {
                    Console.WriteLine("New client connected: {0}", remoteEP.Address);
                    ips.Add(remoteEP);
                }
                messages.Enqueue(Tuple.Create(message, remoteEP));
            }
        }
        static void RunReplier() {
            foreach (var reply in replies.GetConsumingEnumerable()) { 
                udpServer.SendMessage(reply.Item1, reply.Item2);
            }
        }

        public static Tuple<long, long> GetTraffic() {
            return Tuple.Create(udpServer.GetTrafficSent(), udpServer.GetTrafficReceived());
        }

        public static void Handle(T obj) {
            Tuple<Message<T>, IPEndPoint> message;
            while (messages.TryDequeue(out message)) {
                var reply = message.Item1.Handle(obj);
                if (reply != null)
                    replies.Add(Tuple.Create(reply, message.Item2));
            }
        }

    }

}