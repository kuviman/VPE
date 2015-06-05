using System;

namespace VitPro.Engine.Examples {

	abstract class ExampleSelectElement : UI.Element {

		public ExampleSelectElement() {}

		public abstract State GetState();

	}

	abstract class RoundSelectElement : ExampleSelectElement {
		public RoundSelectElement(string text, Color? color = null) {
			if (color == null)
				color = new Color(0.9, 0.9, 0.9);
			Text = text;
			TextSize = 30;
			Padding = -0.05;
			var font = new OutlinedFont(Draw.Font as Font);
			font.OutlineColor = Color.Black;
			font.OutlineWidth = 0.1;
			Font = font;
			backColor = color.Value;
		}

		Color backColor;

		public override void Render() {
			RenderState.Push();
			if (Pressed)
				RenderState.Translate(1, -2);
			RenderState.Push();
			RenderState.Color = Color.Black;
			Draw.Circle(Center, Math.Max(Size.X, Size.Y) / 1.7);
			RenderState.Color = backColor;
			if (Hovered)
				RenderState.Color = Color.Mix(RenderState.Color, Color.Sky);
			Draw.Circle(Center, Math.Max(Size.X, Size.Y) / 2);
			RenderState.Pop();
			base.Render();
			RenderState.Pop();
		}
	}

}
