using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace VitPro.Net {

    public interface Message<T> {
        Message<T> Handle(T obj);
    }

    [Serializable]
    public class MessageList<T> : Message<T> {
        List<Message<T>> messages = new List<Message<T>>();

        public void AddMessage(Message<T> message) {
            messages.Add(message);
        }

        public Message<T> Handle(T obj) {
            MessageList<T> reply = new MessageList<T>();
            foreach (var message in messages) {
                var cur = message.Handle(obj);
                if (cur != null)
                    reply.AddMessage(cur);
            }
            if (reply.messages.Count == 0)
                return null;
            return reply;
        }
    }

    [Serializable]
    class MessagePart {
        public long messageId;
        public int partId;
        public int totalParts;
        public byte[] data;

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
        static Dictionary<UdpClient, Dictionary<long, List<MessagePart>>> messages = new Dictionary<UdpClient,Dictionary<long,List<MessagePart>>>();
        public static void SendMessage<T>(this UdpClient client, Message<T> message, IPEndPoint ip) {
            byte[] data = GUtil.Serialize(message);
            if (data.Length < MAX_MESSAGE_SIZE)
                Send(client, data, ip);
            else {
                var messageId = idCounter++;
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
        public static void SendMessage<T>(this UdpClient client, Message<T> message) {
            SendMessage(client, message, null);
        }
        public static Message<T> ReceiveMessage<T>(this UdpClient client, ref IPEndPoint ip) {
            while (true) {
                var o = GUtil.Deserialize<object>(client.Receive(ref ip));
                var message = o as Message<T>;
                if (message != null)
                    return message;
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
                    return GUtil.Deserialize<Message<T>>(data);
                }
            }
        }

        static void Send(UdpClient client, byte[] data, IPEndPoint ip) {
            if (ip == null)
                client.Send(data, data.Length);
            else
                client.Send(data, data.Length, ip);
        }
    }

}