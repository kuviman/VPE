using System;

namespace VitPro {

	/// <summary>
	/// RGBA color.
	/// </summary>
	
	public partial struct Color {

		[Serialize]
		/// <summary>
		/// Gets or sets the red component.
		/// </summary>
		/// <value>The red component.</value>
		public double R { get; set; }

		[Serialize]
		/// <summary>
		/// Gets or sets the green component.
		/// </summary>
		/// <value>The green component.</value>
		public double G { get; set; }

		[Serialize]
		/// <summary>
		/// Gets or sets the blue component.
		/// </summary>
		/// <value>The blue component.</value>
		public double B { get; set; }

		[Serialize]
		/// <summary>
		/// Gets or sets the alpha component.
		/// </summary>
		/// <value>The alpha component.</value>
		public double A { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Color"/>.
		/// </summary>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="a">The alpha component.</param>
		public Color(double r, double g, double b, double a = 1) : this() {
			R = r;
			G = g;
			B = b;
			A = a;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="VitPro.Color"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="VitPro.Color"/>.</returns>
		public override string ToString() {
			return string.Format("({0}; {1}; {2}; {3})", R, G, B, A);
		}

	}

}
