using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace VitPro.Net {

	
	public class Message {
		[Serialize]
		public int Sender { get; internal set; }
	}

    
	class MessagePart {
		
		[Serialize]
        public long messageId;

		[Serialize]
        public int partId;

		[Serialize]
        public int totalParts;

		[Serialize]
        public byte[] data;

		MessagePart() {}

        public MessagePart(long messageId, int partId, int totalParts, byte[] data) {
            this.messageId = messageId;
            this.partId = partId;
            this.totalParts = totalParts;
            this.data = data;
        }
    }

    static class UdpServerExt {
        const int MAX_MESSAGE_SIZE = 1000;
        static long idCounter = 0;
		static Dictionary<int, IPEndPoint> ips = new Dictionary<int, IPEndPoint>();
        static Dictionary<UdpClient, long> trafficReceived = new Dictionary<UdpClient, long>();
        static Dictionary<UdpClient, long> trafficSent = new Dictionary<UdpClient, long>();
        static Dictionary<UdpClient, Dictionary<long, List<MessagePart>>> messages = new Dictionary<UdpClient,Dictionary<long,List<MessagePart>>>();
        static object lc = new object();

        public static long GetTrafficReceived(this UdpClient client) {
            if (trafficReceived.ContainsKey(client))
                return trafficReceived[client];
            else
                return 0;
        }
        public static long GetTrafficSent(this UdpClient client) {
            if (trafficSent.ContainsKey(client))
                return trafficSent[client];
            else
                return 0;
        }

		public static void SendMessage(this UdpClient client, Message message, IPEndPoint ip) {
            Console.WriteLine("SENDING MESSAGE OF TYPE {0}", message.GetType());
            byte[] data = GUtil.Serialize(message);
            if (data.Length < MAX_MESSAGE_SIZE)
                Send(client, data, ip);
            else {
                long messageId = GRandom.Next();
                int totalParts = (data.Length + MAX_MESSAGE_SIZE - 1) / MAX_MESSAGE_SIZE;
                for (int i = 0; i * MAX_MESSAGE_SIZE < data.Length; i++) {
                    var dataPart = new byte[Math.Min(MAX_MESSAGE_SIZE, data.Length - i * MAX_MESSAGE_SIZE)];
                    for (int j = 0; j < dataPart.Length; j++)
                        dataPart[j] = data[i * MAX_MESSAGE_SIZE + j];
                    var part = GUtil.Serialize(new MessagePart(messageId, i, totalParts, dataPart));
                    Send(client, part, ip);
                }
            }
        }
		public static void SendMessage(this UdpClient client, Message message, int ipHash) {
            lock (lc) {
                var ip = ips[ipHash];
                SendMessage(client, message, ip);
            }
		}
		public static void SendMessage(this UdpClient client, Message message) {
            SendMessage(client, message, null);
        }

		static T GetSender<T>(T message, IPEndPoint ip) where T : Message {
			ips[ip.GetHashCode()] = ip;
			message.Sender = ip.GetHashCode();
            Console.WriteLine("GOT MESSAGE OF TYPE {0}", message.GetType());
			return message;
		}

		public static T ReceiveMessage<T>(this UdpClient client, ref IPEndPoint ip) where T : Message {
            while (true) {
                var receivedData = client.Receive(ref ip);
                lock (lc) {
                    trafficReceived[client] = GetTrafficReceived(client) + receivedData.Length;
                    var o = GUtil.Deserialize<object>(receivedData);
                    var message = o as T;
                    if (message != null)
                        return GetSender(message, ip);
                    var part = o as MessagePart;
                    if (!messages.ContainsKey(client)) {
                        messages[client] = new Dictionary<long, List<MessagePart>>();
                    }
                    if (!messages[client].ContainsKey(part.messageId))
                        messages[client][part.messageId] = new List<MessagePart>();
                    var parts = messages[client][part.messageId];
                    parts.Add(part);
                    if (parts.Count == part.totalParts) {
                        var size = 0;
                        foreach (var p in parts)
                            size += p.data.Length;
                        var data = new byte[size];
                        foreach (var p in parts) {
                            for (int i = 0; i < p.data.Length; i++)
                                data[p.partId * MAX_MESSAGE_SIZE + i] = p.data[i];
                        }
                        messages[client].Remove(part.messageId);
                        return GetSender(GUtil.Deserialize<T>(data), ip);
                    }
                }
            }
        }

        static void Send(UdpClient client, byte[] data, IPEndPoint ip) {
            trafficSent[client] = GetTrafficSent(client) + data.Length;
            if (ip == null)
                client.Send(data, data.Length);
            else
                client.Send(data, data.Length, ip);
        }
    }

}