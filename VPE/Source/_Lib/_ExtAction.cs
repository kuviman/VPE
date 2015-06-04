using System;

namespace VitPro {

	/// <summary>
	/// Provides extensions to Action delegates.
	/// </summary>
	public static class ExtAction {

		/// <summary>
		/// Apply action if not null.
		/// </summary>
		public static void Apply(this Action f) {
			if (f != null)
				f();
		}

		/// <summary>
		/// Apply action if not null.
		/// </summary>
		public static void Apply<T>(this Action<T> f, T arg) {
			if (f != null)
				f(arg);
		}

		/// <summary>
		/// Apply action if not null.
		/// </summary>
		public static void Apply<T1, T2>(this Action<T1, T2> f, T1 arg1, T2 arg2) {
			if (f != null)
				f(arg1, arg2);
		}

		/// <summary>
		/// Apply action if not null.
		/// </summary>
		public static void Apply<T1, T2, T3>(this Action<T1, T2, T3> f, T1 arg1, T2 arg2, T3 arg3) {
			if (f != null)
				f(arg1, arg2, arg3);
		}

	}

}
