using System;

namespace VitPro {

	/// <summary>
	/// Provides additional mathematical functions.
	/// </summary>
	public static partial class GMath {

		/// <summary>
		/// Returns x * x.
		/// </summary>
		public static int Sqr(int x) {
			return x * x;
		}

		/// <summary>
		/// Returns x * x.
		/// </summary>
		public static long Sqr(long x) {
			return x * x;
		}

		/// <summary>
		/// Returns x * x.
		/// </summary>
		public static double Sqr(double x) {
			return x * x;
		}

		/// <summary>
		/// Return fractional part of x.
		/// </summary>
		public static double Frac(double x) {
			return x - Math.Floor(x);
		}

		/// <summary>
		/// Returns the largest integer less than or equal to x.
		/// </summary>
		public static int Floor(double x) {
			return (int)Math.Floor(x);
		}

		/// <summary>
		/// Floor each component.
		/// </summary>
		public static Vec2i Floor(Vec2 v) {
			return new Vec2i(Floor(v.X), Floor(v.Y));
		}

		/// <summary>
		/// Returns the smallest integral value that is greater than or equal to x.
		/// </summary>
		public static int Ceil(double x) {
			return (int)Math.Ceiling(x);
		}

		/// <summary>
		/// Ceil each component.
		/// </summary>
		public static Vec2i Ceil(Vec2 v) {
			return new Vec2i(Ceil(v.X), Ceil(v.Y));
		}

		/// <summary>
		/// Convert radians into degrees.
		/// </summary>
		public static double Degrees(double a) {
			return 180 / Math.PI * a;
		}

		/// <summary>
		/// Convert degrees into radians.
		/// </summary>
		public static double Radians(double a) {
			return Math.PI / 180 * a;
		}

		/// <summary>
		/// True mod.
		/// </summary>
		public static int Mod(int a, int b) {
			int res = a % b;
			if (res < 0)
				res += b;
			return res;
		}

		/// <summary>
		/// True mod.
		/// </summary>
		public static double Mod(double a, double b) {
			double res = Math.IEEERemainder(a, b);
			if (res < 0)
				res += b;
			return res;
		}

	}

}
