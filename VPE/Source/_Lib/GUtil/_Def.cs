using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using BinaryFormatter = System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;

namespace VitPro {

    /// <summary>
    /// Provides utility functions.
    /// </summary>
    public static partial class GUtil {

        /// <summary>
        /// Swap a with b.
        /// </summary>
        public static void Swap<T>(ref T a, ref T b) {
            T c = a;
            a = b;
            b = c;
        }

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

        /// <summary>
        /// Reads contents of a text file.
        /// </summary>
        /// <param name="path">File to read.</param>
        public static string ReadFile(string path) {
            using (var f = new StreamReader(path))
                return f.ReadToEnd();
        }

        /// <summary>
        /// Read all the data from the stream.
        /// </summary>
        /// <param name="stream">Stream.</param>
        public static byte[] ReadFully(this System.IO.Stream stream) {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream()) {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }

        public static byte[] Compress(byte[] data) {
            using (var ms = new MemoryStream()) {
                using (var gzip = new GZipStream(ms, CompressionLevel.Optimal)) {
                    gzip.Write(data, 0, data.Length);
                }
                data = ms.ToArray();
            }
            return data;
        }
        public static byte[] Decompress(byte[] data) {
            // the trick is to read the last 4 bytes to get the length
            // gzip appends this to the array when compressing
            var lengthBuffer = new byte[4];
            Array.Copy(data, data.Length - 4, lengthBuffer, 0, 4);
            int uncompressedSize = BitConverter.ToInt32(lengthBuffer, 0);
            var buffer = new byte[uncompressedSize];
            using (var ms = new MemoryStream(data)) {
                using (var gzip = new GZipStream(ms, CompressionMode.Decompress)) {
                    gzip.Read(buffer, 0, uncompressedSize);
                }
            }
            return buffer;
        }

    }

}
