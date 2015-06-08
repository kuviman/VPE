using System;

namespace VitPro.Engine {

	partial class Draw {

        static void InitText() {
            Font = new Font("Courier New", VitPro.Engine.Font.Style.Bold);
        }

		/// <summary>
		/// Gets or sets the default font.
		/// </summary>
		/// <value>The font.</value>
		public static IFont Font { get; set; }

		/// <summary>
		/// Render the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		public static void Text(string text) {
			Font.Render(text);
		}

		/// <summary>
		/// Render the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="ax">X align.</param>
		/// <param name="ay">Y align.</param>
		public static void Text(string text, double ax, double ay = 0) {
			Font.Render(text, ax, ay);
		}

	}

}
