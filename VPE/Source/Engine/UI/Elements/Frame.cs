using System;
using System.Collections.Generic;

namespace VitPro.Engine.UI {

	/// <summary>
	/// UI Frame.
	/// </summary>
	public class Frame : Element {

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.Frame"/> class.
		/// </summary>
		public Frame() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.Frame"/> class.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public Frame(double width, double height) {
			Size = new Vec2(width, height);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.Frame"/> class.
		/// </summary>
		/// <param name="size">Size.</param>
		public Frame(Vec2 size) {
			Size = size;
		}

	}

}
