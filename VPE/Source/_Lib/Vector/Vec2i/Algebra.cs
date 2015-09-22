using System;

namespace VitPro {

	partial struct Vec2i {
		
		public static Vec2i operator +(Vec2i v) {
			return new Vec2i(+v.X, +v.Y);
		}
		public static Vec2i operator -(Vec2i v) {
			return new Vec2i(-v.X, -v.Y);
		}
		
		public static Vec2i operator +(Vec2i a, Vec2i b) {
			return new Vec2i(a.X + b.X, a.Y + b.Y);
		}
		public static Vec2i operator -(Vec2i a, Vec2i b) {
			return new Vec2i(a.X - b.X, a.Y - b.Y);
		}

		public static Vec2i operator *(Vec2i v, int k) {
			return new Vec2i(v.X * k, v.Y * k);
		}
		public static Vec2i operator *(int k, Vec2i v) {
			return new Vec2i(k * v.X, k * v.Y);
		}

		public static Vec2i operator /(Vec2i v, int k) {
			return new Vec2i(v.X / k, v.Y / k);
		}

		/// <summary>
		/// Dot product.
		/// </summary>
		public static int Dot(Vec2i a, Vec2i b) {
			return a.X * b.X + a.Y * b.Y;
		}

		/// <summary>
		/// Skew product.
		/// </summary>
		public static int Skew(Vec2i a, Vec2i b) {
			return a.X * b.Y - a.Y * b.X;
		}

		/// <summary>
		/// Multiply two vectors componentwise.
		/// </summary>
		/// <param name="a">First vector.</param>
		/// <param name="b">Second vector.</param>
		public static Vec2i CompMult(Vec2i a, Vec2i b) {
			return new Vec2i(a.X * b.X, a.Y * b.Y);
		}

		/// <summary>
		/// Divide one vector by another componentwise.
		/// </summary>
		/// <param name="a">First vector.</param>
		/// <param name="b">Second vector.</param>
		public static Vec2i CompDiv(Vec2i a, Vec2i b) {
			return new Vec2i(a.X / b.X, a.Y / b.Y);
		}

		
		/// <summary>
		/// Gets the square length of this vector.
		/// </summary>
		/// <value>The square length.</value>
		public int SquareLength {
			get { return Dot(this, this); }
		}

	}
	
}
