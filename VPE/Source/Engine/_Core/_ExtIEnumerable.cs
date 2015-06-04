using System;
using System.Collections.Generic;

namespace VitPro.Engine {

	/// <summary>
	/// Provides extensions for IEnumerable interface.
	/// </summary>
	public static class ExtIEnumerable {

		/// <summary>
		/// Update every element from a collection.
		/// </summary>
		public static void Update<T>(this IEnumerable<T> list, double dt)
			where T: IUpdateable {
			foreach (var a in list)
				a.Update(dt);
		}

		/// <summary>
		/// Render every element from a collection.
		/// </summary>
		public static void Render<T>(this IEnumerable<T> list)
			where T: IRenderable {
			foreach (var a in list)
				a.Render();
		}

	}

}
