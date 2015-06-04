using System;

namespace VitPro.Engine {

	/// <summary>
	/// Font interface.
	/// </summary>
	public interface IFont {

		/// <summary>
		/// Measure the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		double Measure(string text);

		/// <summary>
		/// Render the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		void Render(string text);

	}

	/// <summary>
	/// Font extension methods.
	/// </summary>
	public static class FontExt {
		
		/// <summary>
		/// Render the specified text.
		/// </summary>
		/// <param name="font">Font.</param>
		/// <param name="text">Text.</param>
		/// <param name="ax">X align.</param>
		/// <param name="ay">Y align.</param>
		public static void Render(this IFont font, string text, double ax, double ay = 0) {
			RenderState.Push();
			RenderState.Origin(ax * font.Measure(text), ay);
			font.Render(text);
			RenderState.Pop();
		}

	}
	
}
