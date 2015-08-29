using System;
using System.Collections.Concurrent;
using System.IO;
using BinaryFormatter = System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;

namespace VitPro {

    partial class GUtil {

        public static byte[] Serialize(object o) {
            var f = new MemoryStream();
            var fmt = new BinaryFormatter();
            fmt.Serialize(f, o);
            var bytes = f.ToArray();
            return Compress(bytes);
        }

        public static T Deserialize<T>(byte[] bytes) {
            bytes = Decompress(bytes);
            var f = new MemoryStream(bytes);
            var fmt = new BinaryFormatter();
            return (T)fmt.Deserialize(f);
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