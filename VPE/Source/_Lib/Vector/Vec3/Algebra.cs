using System;

namespace VitPro {

	partial struct Vec3 {
		
		public static Vec3 operator +(Vec3 v) {
			return new Vec3(+v.X, +v.Y, +v.Z);
		}
		public static Vec3 operator -(Vec3 v) {
			return new Vec3(-v.X, -v.Y, -v.Z);
		}
		
		public static Vec3 operator +(Vec3 a, Vec3 b) {
			return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}
		public static Vec3 operator -(Vec3 a, Vec3 b) {
			return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static Vec3 operator *(Vec3 v, double k) {
			return new Vec3(v.X * k, v.Y * k, v.Z * k);
		}
		public static Vec3 operator *(double k, Vec3 v) {
			return new Vec3(k * v.X, k * v.Y, k * v.Z);
		}

		public static Vec3 operator /(Vec3 v, double k) {
			return new Vec3(v.X / k, v.Y / k, v.Z / k);
		}

		/// <summary>
		/// Dot product.
		/// </summary>
		public static double Dot(Vec3 a, Vec3 b) {
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		/// <summary>
		/// Cross product.
		/// </summary>
		public static Vec3 Cross(Vec3 a, Vec3 b) {
			return new Vec3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
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
        public Vec3 Unit {
            get {
                var len = Length;
                if (len < 1e-9)
                    return this;
                return this / len;
            }
        }

	}
	
}
