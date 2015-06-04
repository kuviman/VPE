using System;

namespace VitPro {

	partial struct Color {

		public static Color Black = new Color(0, 0, 0);
		public static Color White = new Color(1, 1, 1);

		public static Color TransparentWhite = new Color(1, 1, 1, 0);
		public static Color TransparentBlack = new Color(0, 0, 0, 0);

		public static Color Gray = new Color(0.5, 0.5, 0.5);
		public static Color LightGray = new Color(0.75, 0.75, 0.75);
		public static Color DarkGray = new Color(0.25, 0.25, 0.25);

		public static Color Red = new Color(1, 0, 0);
		public static Color Green = new Color(0, 1, 0);
		public static Color Blue = new Color(0, 0, 1);

		public static Color Yellow = new Color(1, 1, 0);
		public static Color Cyan = new Color(0, 1, 1);
		public static Color Magenta = new Color(1, 0, 1);

		public static Color Orange = new Color(1, 0.5, 0);

		public static Color Sky = new Color(0.8, 0.8, 1);
		
	}

}
