using System;
using System.IO;
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
            return f.ToArray();
        }

        public static T Deserialize<T>(byte[] bytes) {
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

	}

}
