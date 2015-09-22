using System;

namespace VitPro {

	/// <summary>
	/// 2d vector with double coordinates.
	/// </summary>
	
	public partial struct Vec2 {

		[Serialize]
		/// <summary>
		/// Gets or sets the x coordinate.
		/// </summary>
		/// <value>The x coordinate.</value>
		public double X { get; set; }

		[Serialize]
		/// <summary>
		/// Gets or sets the y coordinate.
		/// </summary>
		/// <value>The y coordinate.</value>
		public double Y { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Vec2"/> struct.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public Vec2(double x, double y) : this() {
			X = x;
			Y = y;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="VitPro.Vec2"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="VitPro.Vec2"/>.</returns>
		public override string ToString() {
			return string.Format("({0}; {1})", X, Y);
		}

		/// <summary>
		/// Gets the zero vector.
		/// </summary>
		/// <value>The zero vector.</value>
        public static Vec2 Zero { get { return new Vec2(0, 0); } }

        static Vec2 _ortX = new Vec2(1, 0);

		/// <summary>
		/// Gets the x ort vector.
		/// </summary>
		/// <value>The x ort vector.</value>
		public static Vec2 OrtX { get { return _ortX; } }

        static Vec2 _ortY = new Vec2(0, 1);

		/// <summary>
		/// Gets the y ort vector.
		/// </summary>
		/// <value>The y ort vector.</value>
        public static Vec2 OrtY { get { return _ortY; } }

	}

}
