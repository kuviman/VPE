using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace VitPro.Net {

    public class Client<T> {

		T model;
        UdpClient udpClient;
        IPEndPoint ep;

        public Client(T model, string address, int port) {
			this.model = model;
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

        public void Handle() {
            Message<T> message;
            while (queue.TryDequeue(out message)) {
                var reply = message.Handle(model);
                if (reply != null)
                    udpClient.SendMessage(reply);
            }
        }

    }

}