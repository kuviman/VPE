using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Web.Script.Serialization;
using System.IO;
using BinaryFormatter = System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;
using System.Xml.Serialization;

namespace VitPro {

    partial class GUtil {

		public static byte[] Serialize(object o, bool compatible = true) {
			var f = new MemoryStream();
			var fmt = new BinaryFormatter();
            fmt.Serialize(f, o);
			return Compress(f.ToArray());
        }

		public static T Deserialize<T>(byte[] bytes, bool compatible = true) {
            bytes = Decompress(bytes);
			var f = new MemoryStream(bytes);
			var fmt = new BinaryFormatter();
			var obj = fmt.Deserialize(f);
			return (T)obj;
        }

        /// <summary>
        /// Serialize object into a file.
        /// </summary>
        public static void Dump(object o, string path) {
            using (var f = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                var fmt = new BinaryFormatter();
                fmt.Serialize(f, o);
            }
        }

        /// <summary>
        /// Deserialize object from a file.
        /// </summary>
        public static T Load<T>(string path) {
            using (var f = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                var fmt = new BinaryFormatter();
                return (T)fmt.Deserialize(f);
            }
        }

    }

}