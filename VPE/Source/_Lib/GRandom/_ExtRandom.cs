using System;
using System.Collections.Generic;

namespace VitPro {

	/// <summary>
	/// Provides extensions for Random class
	/// </summary>
	public static class ExtRandom {

		/// <summary>
		/// Returns boolean with the given probability.
		/// </summary>
		/// <param name="p">Event probability.</param>
		public static bool Probable(this Random rnd, double p) {
			return rnd.NextDouble() < p;
		}

		/// <summary>
		/// Chooses a random item from the list.
		/// </summary>
		/// <param name="a">List to choose from.</param>
		public static T Choice<T>(this Random rnd, IList<T> a) {
			return a[rnd.Next(a.Count)];
		}

		/// <summary>
		/// Returns either true or false.
		/// </summary>
		public static bool Coin(this Random rnd) {
			return rnd.Next(2) == 0;
		}

		/// <summary>
		/// Returns random double from the given interval.
		/// </summary>
		/// <param name="minValue">Minimal value.</param>
		/// <param name="maxValue">Maximal value.</param>
		public static double NextDouble(this Random rnd, double minValue, double maxValue) {
			return minValue + rnd.NextDouble() * (maxValue - minValue);
		}

	}

}