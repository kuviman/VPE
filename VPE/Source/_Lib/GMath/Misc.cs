using System;

namespace VitPro
{
	partial class GMath {

		/// <summary>
		/// Returns minimal angle difference between a1 and a2, negative if rotation is clockwise.
		/// </summary>
		public static double AngleDifference(double a1, double a2) {
			double diff = Math.IEEERemainder(a1 - a2, 2 * Math.PI);
			if (diff > Math.PI)
				diff -= 2 * Math.PI;
			return diff;
		}

        public static int DivDown(int a, int b) {
            if (a < 0)
                return -DivUp(-a, b);
            return a / b;
        }

        public static int DivUp(int a, int b) {
            if (a < 0)
                return -DivDown(-a, b);
            return (a + b - 1) / b;
        }

	}
}
