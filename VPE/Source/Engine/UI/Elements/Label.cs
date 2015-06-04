using System;
using System.Collections.Generic;

namespace VitPro.Engine.UI {

	/// <summary>
	/// Label UI element.
	/// </summary>
	public class Label : Element {

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.Label"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="size">Size.</param>
		public Label(string text, double size = 20) {
			Text = text;
			TextSize = size;
		}

	}

}
