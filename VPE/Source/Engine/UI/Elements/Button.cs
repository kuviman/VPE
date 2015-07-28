using System;

namespace VitPro.Engine.UI {

	/// <summary>
	/// UI button.
	/// </summary>
	public class Button : Element {

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.Button"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="size">Size.</param>
		/// <param name="action">Action.</param>
		public Button(string text, Action action, double size = 20) {
			if (action != null)
                OnClick += action;
			Text = text;
			TextSize = size;
			TextColor = Color.Gray;
			HoveredColor = Color.Sky;
			UnhoveredColor = new Color(0.9, 0.9, 0.9);
			PressOffset = new Vec2(1, -2);
			BorderColor = Color.Gray;
		}

		/// <summary>
		/// Gets or sets the color of the button when you hover mouse over it.
		/// </summary>
		/// <value>The color.</value>
		public Color HoveredColor { get; set; }

		/// <summary>
		/// Gets or sets the color of the button when mouse is not hovered over it.
		/// </summary>
		/// <value>The color.</value>
		public Color UnhoveredColor { get; set; }

		/// <summary>
		/// Update this element.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public override void Update(double dt) {
			if (Hovered)
				BackgroundColor = HoveredColor;
			else
				BackgroundColor = UnhoveredColor;
			base.Update(dt);
		}

		/// <summary>
		/// Gets or sets the offset of the button while pressed.
		/// </summary>
		/// <value>The offset.</value>
		public Vec2 PressOffset { get; set; }

		/// <summary>
		/// Render this element.
		/// </summary>
		public override void Render() {
			RenderState.Push();
			if (Pressed)
				RenderState.Translate(PressOffset);
			base.Render();
			RenderState.Pop();
		}

	}

}
