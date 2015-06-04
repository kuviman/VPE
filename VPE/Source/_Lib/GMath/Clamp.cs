using System;

namespace VitPro
{
	partial class GMath {

		/// <summary>
		/// Returns a if x &lt; a, b if x &gt; b, x otherwise.
		/// </summary>
		public static int Clamp(int x, int a, int b) {
			if (x < a)
				return a;
			if (x > b)
				return b;
			return x;
		}
		/// <summary>
		/// Returns a if x &lt; a, b if x &gt; b, x otherwise.
		/// </summary>
		public static long Clamp(long x, long a, long b) {
			if (x < a)
				return a;
			if (x > b)
				return b;
			return x;
		}
		/// <summary>
		/// Returns a if x &lt; a, b if x &gt; b, x otherwise.
		/// </summary>
		public static double Clamp(double x, double a, double b) {
			if (x < a)
				return a;
			if (x > b)
				return b;
			return x;
		}

		/// <summary>
		/// Returns a if x &lt; -a, b if x &gt; a, x otherwise.
		/// </summary>
		public static double Clamp(double x, double a) {
			return Clamp(x, -a, a);
		}
		/// <summary>
		/// Returns a if x &lt; -a, b if x &gt; a, x otherwise.
		/// </summary>
		public static int Clamp(int x, int a) {
			return Clamp(x, -a, a);
		}
		/// <summary>
		/// Returns a if x &lt; -a, b if x &gt; a, x otherwise.
		/// </summary>
		public static long Clamp(long x, long a) {
			return Clamp(x, -a, a);
		}

	}
}
