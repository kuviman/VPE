using System;
using System.Net.Sockets;

namespace VitPro.Net {

    public class NetException : Exception {
        public NetException(string message) : base(message) { }
        public NetException(string message, Exception reason) : base(message, reason) { }
    }

}