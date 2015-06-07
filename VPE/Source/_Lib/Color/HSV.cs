using System;

namespace VitPro {

	partial struct Color {
		
		/// <summary>
		/// Create color from HSV components.
		/// </summary>
		/// <param name="h">Hue.</param>
		/// <param name="s">Saturation.</param>
		/// <param name="v">Value.</param>
		/// <param name="a">Alpha.</param>
		public static Color FromHSV(double h, double s, double v, double a = 1) {
			h -= Math.Floor(h);
			double r, g, b;
			double f = h * 6 - Math.Floor(h * 6);
			double p = v * (1 - s);
			double q = v * (1 - f * s);
			double t = v * (1 - (1 - f) * s);
			if (h * 6 < 1) {
				r = v; g = t; b = p;
			} else if (h * 6 < 2) {
				r = q; g = v; b = p;
			} else if (h * 6 < 3) {
				r = p; g = v; b = t;
			} else if (h * 6 < 4) {
				r = p; g = q; b = v;
			} else if (h * 6 < 5) {
				r = t; g = p; b = v;
			} else {
				r = v; g = p; b = q;
			}
			return new Color(r, g, b, a);
		}

		Vec3 toHSV() {
			double Cmax = Math.Max(R, Math.Max(G, B));
			double Cmin = Math.Min(R, Math.Min(G, B));
			double d = Cmax - Cmin;
			double h, s, v;
			if (d == 0.0)
				h = 0.0;
			else if (Cmax == R)
				h = GMath.Mod(((G - B) / d + 6.0) / 6.0, 1.0);
			else if (Cmax == G)
				h = ((B - R) / d + 2.0) / 6.0;
			else
				h = ((R - G) / d + 4.0) / 6.0;
			if (Cmax == 0.0)
				s = 0.0;
			else
				s = d / Cmax;
			v = Cmax;
			return new Vec3(h, s, v);
		}

		/// <summary>
		/// Gets the hue.
		/// </summary>
		/// <value>The hue.</value>
		public double H {
			get { return toHSV().X; }
		}

		/// <summary>
		/// Gets the saturation.
		/// </summary>
		/// <value>The saturation.</value>
		public double S {
			get { return toHSV().Y; }
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public double V {
			get { return toHSV().Z; }
		}

	}

}
