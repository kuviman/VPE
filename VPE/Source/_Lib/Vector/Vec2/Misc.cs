using System;

namespace VitPro {

	partial struct Vec2 {

		
		/// <summary>
		/// Get the square length of the vector.
		/// </summary>
		public double SqrLength {
			get { return X * X + Y * Y; }
		}

		
		/// <summary>
		/// Get the argument of the vector.
		/// </summary>
		public double Arg {
			get { return Math.Atan2(Y, X); }
		}

		/// <summary>
		/// Rotate a vector.
		/// </summary>
		/// <param name="a">Vector to rotate.</param>
		/// <param name="angle">Ratation angle.</param>
		/// <returns>Rotated vector.</returns>
		public static Vec2 Rotate(Vec2 a, double angle) {
			double s = Math.Sin(angle);
			double c = Math.Cos(angle);
			return new Vec2(a.X * c - a.Y * s, a.X * s + a.Y * c);
		}

		/// <summary>
		/// Rotate a vector 90 degrees counter-clockwise.
		/// </summary>
		/// <param name="a">Vector to rotate.</param>
		public static Vec2 Rotate90(Vec2 a) {
			return new Vec2(-a.Y, a.X);
		}

		/// <summary>
		/// Clamp the length of a vector.
		/// </summary>
		/// <param name="a">Vector to clamp.</param>
		/// <param name="maxlen">Maximal length.</param>
		/// <returns>Clamped vector.</returns>
		public static Vec2 Clamp(Vec2 a, double maxlen) {
			if (a.Length <= maxlen)
				return a;
			else
				return a.Unit * maxlen;
		}

		public static explicit operator Vec2i(Vec2 v) {
			return new Vec2i((int)v.X, (int)v.Y);
		}
		public static implicit operator Vec2(Vec2i v) {
			return new Vec2(v.X, v.Y);
		}

	}

}
