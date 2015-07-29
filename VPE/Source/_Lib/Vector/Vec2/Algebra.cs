using System;

namespace VitPro {

	partial struct Vec2 {
		
		public static Vec2 operator +(Vec2 v) {
			return new Vec2(+v.X, +v.Y);
		}
		public static Vec2 operator -(Vec2 v) {
			return new Vec2(-v.X, -v.Y);
		}
		
		public static Vec2 operator +(Vec2 a, Vec2 b) {
			return new Vec2(a.X + b.X, a.Y + b.Y);
		}
		public static Vec2 operator -(Vec2 a, Vec2 b) {
			return new Vec2(a.X - b.X, a.Y - b.Y);
		}

		public static Vec2 operator *(Vec2 v, double k) {
			return new Vec2(v.X * k, v.Y * k);
		}
		public static Vec2 operator *(double k, Vec2 v) {
			return new Vec2(k * v.X, k * v.Y);
		}

		public static Vec2 operator /(Vec2 v, double k) {
			return new Vec2(v.X / k, v.Y / k);
		}

		/// <summary>
		/// Dot product.
		/// </summary>
		public static double Dot(Vec2 a, Vec2 b) {
			return a.X * b.X + a.Y * b.Y;
		}

		/// <summary>
		/// Skew product.
		/// </summary>
		public static double Skew(Vec2 a, Vec2 b) {
			return a.X * b.Y - a.Y * b.X;
		}

		/// <summary>
		/// Multiply two vectors componentwise.
		/// </summary>
		/// <param name="a">First vector.</param>
		/// <param name="b">Second vector.</param>
		public static Vec2 CompMult(Vec2 a, Vec2 b) {
			return new Vec2(a.X * b.X, a.Y * b.Y);
		}

		/// <summary>
		/// Divide one vector by another componentwise.
		/// </summary>
		/// <param name="a">First vector.</param>
		/// <param name="b">Second vector.</param>
		public static Vec2 CompDiv(Vec2 a, Vec2 b) {
			return new Vec2(a.X / b.X, a.Y / b.Y);
		}

		/// <summary>
		/// Gets the square length of this vector.
		/// </summary>
		/// <value>The square length.</value>
		public double SquareLength {
			get { return Dot(this, this); }
		}

		/// <summary>
		/// Gets the length of this vector.
		/// </summary>
		/// <value>The length.</value>
		public double Length {
			get { return Math.Sqrt(SquareLength); }
		}

		/// <summary>
		/// Gets the unit vector.
		/// </summary>
		/// <value>The unit vector.</value>
		public Vec2 Unit {
			get {
                var len = Length;
                if (len < 1e-9)
                    return this;
                return this / len;
            }
		}

	}
	
}
