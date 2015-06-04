using System;

namespace VitPro {

	/// <summary>
	/// 3d vector with double coordinates.
	/// </summary>
	[Serializable]
	public partial struct Vec3 {

		/// <summary>
		/// Gets or sets the x coordinate.
		/// </summary>
		/// <value>The x coordinate.</value>
		public double X { get; set; }

		/// <summary>
		/// Gets or sets the y coordinate.
		/// </summary>
		/// <value>The y coordinate.</value>
		public double Y { get; set; }

		/// <summary>
		/// Gets or sets the z coordinate.
		/// </summary>
		/// <value>The z coordinate.</value>
		public double Z { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Vec3"/> struct.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public Vec3(double x, double y, double z) : this() {
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="VitPro.Vec3"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="VitPro.Vec3"/>.</returns>
		public override string ToString() {
			return string.Format("({0}; {1}; {2})", X, Y, Z);
		}

		/// <summary>
		/// Gets the zero vector.
		/// </summary>
		/// <value>The zero vector.</value>
        public static Vec3 Zero { get { return new Vec3(0, 0, 0); } }
        
        static Vec3 _ortX = new Vec3(1, 0, 0);
        static Vec3 _ortY = new Vec3(0, 1, 0);
        static Vec3 _ortZ = new Vec3(0, 0, 1);

		/// <summary>
		/// Gets the x ort vector.
		/// </summary>
		/// <value>The x ort vector.</value>
		public static Vec3 OrtX { get { return _ortX; } }

		/// <summary>
		/// Gets the y ort vector.
		/// </summary>
		/// <value>The y ort vector.</value>
		public static Vec3 OrtY { get { return _ortY; } }

		/// <summary>
		/// Gets the z ort vector.
		/// </summary>
		/// <value>The z ort vector.</value>
		public static Vec3 OrtZ { get { return _ortZ; } }

	}

}
