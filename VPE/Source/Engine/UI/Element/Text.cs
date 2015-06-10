using System;

namespace VitPro.Engine.UI {

	partial class Element {

		void InitText() {
			TextSize = 20;
			TextAlign = 0.5;
			TextColor = new Color(1, 1, 1, 1);
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the size of the text.
		/// </summary>
		/// <value>The size of the text.</value>
		public double TextSize { get; set; }

		/// <summary>
		/// Gets or sets the text align.
		/// </summary>
		/// <value>The text align.</value>
		public double TextAlign { get; set; }

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color TextColor { get; set; }

		/// <summary>
		/// Gets or sets the font (or null to use default font).
		/// </summary>
		/// <value>The font.</value>
		public IFont Font { get; set; }

		internal IFont RealFont { get { return Font == null ? Draw.Font : Font; } }

		void UpdateText() {
			if (Text == null)
				return;
			var padding = Padding * TextSize;
			Size = new Vec2(padding, padding) * 2;
			foreach (var line in Text.Split('\n'))
				Size = new Vec2(Math.Max(Size.X, padding * 2 + TextSize * RealFont.Measure(line)), Size.Y + TextSize);
		}

		void RenderText() {
			if (Text == null)
				return;
			RenderState.Push();
			IFont font = RealFont;
			var padding = Padding * TextSize;
			RenderState.Translate(TopLeft + new Vec2(TextAlign * (Size.X - 2 * padding) + padding, -padding));
			RenderState.Scale(TextSize);
			RenderState.Color = TextColor;
			foreach (var line in Text.Split('\n')) {
				RealFont.Render(line, TextAlign, 1);
				RenderState.Translate(0, -1);
			}

			RenderState.Pop();
		}

	}

}
