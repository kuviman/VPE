using System;

namespace VitPro.Engine.UI {

	partial class Element {
		
		void InitRendering() {
			BackgroundColor = new Color(0, 0, 0, 0);
			BorderWidth = 2;
			BackgroundColor = new Color(0, 0, 0, 0);
		}

		/// <summary>
		/// Gets or sets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public Color BackgroundColor { get; set; }

		/// <summary>
		/// Gets or sets the width of the border.
		/// </summary>
		/// <value>The width of the border.</value>
		public double BorderWidth { get; set; }

		/// <summary>
		/// Gets or sets the color of the border.
		/// </summary>
		/// <value>The color of the border.</value>
		public Color BorderColor { get; set; }

		void InternalRender() {
			RenderState.Push();
			RenderState.Color = BackgroundColor;
			Draw.Rect(BottomLeft, TopRight);
			RenderState.Color = BorderColor;
			Draw.Frame(BottomLeft, TopRight, BorderWidth);
			RenderState.Pop();
			RenderText();
		}

		/// <summary>
		/// Post render.
		/// </summary>
		protected virtual void PostRender() {}

	}

}
