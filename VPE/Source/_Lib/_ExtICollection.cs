using System;
using System.Collections.Generic;

namespace VitPro {

	public static class ExtICollection {

		/// <summary>
		/// Adds the range of objects to the collection.
		/// </summary>
		/// <param name="collection">Collection.</param>
		/// <param name="range">Range of objects.</param>
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range) {
			foreach (var o in range)
				collection.Add(o);
		}

	}

}