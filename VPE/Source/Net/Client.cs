using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace VitPro.Net {

    public class Client<T> {

        UdpClient udpClient;
        IPEndPoint ep;

        public Client(string address, int port) {
            Console.WriteLine("Trying to connect to {0}:{1}", address, port);
            udpClient = new UdpClient();
            ep = new IPEndPoint(IPAddress.Parse(address), port);
            udpClient.Connect(ep);
            var thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();
        }

        ConcurrentQueue<Message<T>> queue = new ConcurrentQueue<Message<T>>();

        void Run() {
            while (true) {
                queue.Enqueue(udpClient.ReceiveMessage<T>(ref ep));
            }
        }

        public Tuple<long, long> GetTraffic() {
            return Tuple.Create(udpClient.GetTrafficSent(), udpClient.GetTrafficReceived());
        }

        public void Send(Message<T> message) {
            udpClient.SendMessage(message);
        }

        public void Handle(T obj) {
            Message<T> message;
            while (queue.TryDequeue(out message)) {
                var reply = message.Handle(obj);
                if (reply != null)
                    udpClient.SendMessage(reply);
            }
        }

    }

}