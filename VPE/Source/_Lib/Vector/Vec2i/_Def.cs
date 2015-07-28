using System;

namespace VitPro {

	/// <summary>
	/// 2d vector with integer coordinates.
	/// </summary>
	[Serializable]
	public partial struct Vec2i {

		/// <summary>
		/// Gets or sets the x coordinate.
		/// </summary>
		/// <value>The x coordinate.</value>
		public int X { get; set; }

		/// <summary>
		/// Gets or sets the y coordinate.
		/// </summary>
		/// <value>The y coordinate.</value>
		public int Y { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Vec2i"/> struct.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public Vec2i(int x, int y) : this() {
			X = x;
			Y = y;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="VitPro.Vec2i"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="VitPro.Vec2i"/>.</returns>
		public override string ToString() {
			return string.Format("({0}; {1})", X, Y);
		}

		/// <summary>
		/// Gets the zero vector.
		/// </summary>
		/// <value>The zero vector.</value>
        public static Vec2i Zero { get { return new Vec2i(0, 0); } }

        static Vec2i _ortX = new Vec2i(1, 0);

		/// <summary>
		/// Gets the x ort vector.
		/// </summary>
		/// <value>The x ort vector.</value>
		public static Vec2i OrtX { get { return _ortX; } }

        static Vec2i _ortY = new Vec2i(0, 1);

		/// <summary>
		/// Gets the y ort vector.
		/// </summary>
		/// <value>The y ort vector.</value>
		public static Vec2i OrtY { get { return _ortY; } }

	}

}
