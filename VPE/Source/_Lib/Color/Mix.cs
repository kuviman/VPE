using System;

namespace VitPro {

	partial struct Color {

		/// <summary>
		/// Mix two colors.
		/// </summary>
		/// <param name="c1">Color 1.</param>
		/// <param name="c2">Color 2.</param>
		public static Color Mix(Color c1, Color c2) {
			return new Color((c1.R + c2.R) / 2, (c1.G + c2.G) / 2, (c1.B + c2.B) / 2, (c1.A + c2.A) / 2);
		}

		/// <summary>
		/// Mix two colors with specified coefficients.
		/// </summary>
		/// <param name="c1">Color 1.</param>
		/// <param name="c2">Color 2.</param>
		/// <param name="k1">Coefficient for color 1.</param>
		/// <param name="k2">Coefficient for color 2.</param>
		public static Color Mix(Color c1, Color c2, double k1, double k2) {
			double sum = k1 + k2;
			return new Color((c1.R * k1 + c2.R * k2) / sum, (c1.G * k1 + c2.G * k2) / sum,
				(c1.B * k1 + c2.B * k2) / sum, (c1.A * k1 + c2.A * k2) / sum);
		}

		/// <summary>
		/// Blend two colors using standard blending.
		/// </summary>
		/// <param name="dst">Destination color.</param>
		/// <param name="src">Source color.</param>
		public static Color Blend(Color dst, Color src) {
			return Mix(dst, src, 1 - src.A, src.A);
		}

		public static Color operator *(Color c1, Color c2) {
			return new Color(c1.R * c2.R, c1.G * c2.G, c1.B * c2.B, c1.A * c2.A);
		}

		/// <summary>
		/// Multiply color's alpha value.
		/// </summary>
		/// <param name="c">Color.</param>
		/// <param name="k">Multiplication factor.</param>
		public static Color MultAlpha(Color c, double k) {
			return new Color(c.R, c.G, c.B, c.A * k);
		}

		public static Color operator +(Color c1, Color c2) {
			return new Color(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B, c1.A + c2.A);
		}

		public static Color operator *(Color c, double a) {
			return new Color(c.R * a, c.G * a, c.B * a, c.A * a);
		}

		public static Color operator /(Color c, double a) {
			return new Color(c.R / a, c.G / a, c.B / a, c.A / a);
		}
	}

}
