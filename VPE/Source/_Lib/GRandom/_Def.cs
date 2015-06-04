using System;
using System.Collections.Generic;

namespace VitPro {

	/// <summary>
	/// Provides global thread-safe random methods.
	/// </summary>
	public static class GRandom {

		static Random gen = new Random();
		static object Lock = new object();

		/// <summary>
		/// Returns boolean with the given probability.
		/// </summary>
		/// <param name="p">Event probability.</param>
		public static bool Probable(double p) {
			lock (Lock)
				return gen.Probable(p);
		}

		/// <summary>
		/// Chooses a random item from the list.
		/// </summary>
		/// <param name="a">List to choose from.</param>
		public static T Choice<T>(IList<T> a) {
			lock (Lock)
				return gen.Choice(a);
		}

		/// <summary>
		/// Returns either true or false.
		/// </summary>
		public static bool Coin() {
			lock (Lock)
				return gen.Coin();
		}

		/// <summary>
		/// Reset the random generator using given seed.
		/// </summary>
		/// <param name="seed">New random seed.</param>
		public static void Seed(int seed) {
			lock (Lock)
				gen = new Random(seed);
		}

		/// <summary>
		/// Returns a nonnegative random number.
		/// </summary>
		public static int Next() {
			lock (Lock)
				return gen.Next();
		}

		/// <summary>
		/// Returns a nonnegative random number less than the specified maximum.
		/// </summary>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.</param>
		public static int Next(int maxValue) {
			lock (Lock)
				return gen.Next(maxValue);
		}

		/// <summary>
		/// Returns a random number within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
		public static int Next(int minValue, int maxValue) {
			lock (Lock)
				return gen.Next(minValue, maxValue);
		}

		/// <summary>
		/// Returns a random number between 0.0 and 1.0.
		/// </summary>
		public static double NextDouble() {
			lock (Lock)
				return gen.NextDouble();
		}

		/// <summary>
		/// Returns random double from the given interval.
		/// </summary>
		/// <param name="minValue">Minimal value.</param>
		/// <param name="maxValue">Maximal value.</param>
		public static double NextDouble(double minValue, double maxValue) {
			lock (Lock)
				return gen.NextDouble(minValue, maxValue);
		}

		/// <summary>
		/// Fills the elements of a specified array of bytes with random numbers.
		/// </summary>
		/// <param name="buffer">An array of bytes to contain random numbers.</param>
		public static void NextBytes(byte[] buffer) {
			lock (Lock)
				gen.NextBytes(buffer);
		}

	}

}
