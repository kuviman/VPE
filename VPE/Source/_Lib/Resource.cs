using System;
using System.Reflection;

namespace VitPro {

	/// <summary>
	/// Provides methods for working with project resources.
	/// </summary>
	public static class Resource {

		static System.IO.Stream Stream(Assembly assembly, string name) {
			return assembly.GetManifestResourceStream(name);
		}

		/// <summary>
		/// Load resource as a stream.
		/// </summary>
		/// <param name="name">Resource name.</param>
		public static System.IO.Stream Stream(string name) {
			return Stream(Assembly.GetCallingAssembly(), name);
		}

		/// <summary>
		/// Load resource as a string.
		/// </summary>
		/// <param name="name">Resource name.</param>
		public static string String(string name) {
			var sr = new System.IO.StreamReader(Stream(Assembly.GetCallingAssembly(), name));
			return sr.ReadToEnd();
		}

	}

}
