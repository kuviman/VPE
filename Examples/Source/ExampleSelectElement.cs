using System;

namespace VitPro.Engine.Examples {

	abstract class ExampleSelectElement : UI.Element {

		public ExampleSelectElement() {}

		public abstract State GetState();

	}

	abstract class RoundSelectElement : ExampleSelectElement {
		public RoundSelectElement(string text, Color? color = null, double size = 30) {
			if (color == null)
				color = new Color(0.9, 0.9, 0.9);
			this.text = text;
			Size = new Vec2(1, 1) * 27;
			var font = new OutlinedFont(Draw.Font as Font);
			font.OutlineColor = Color.Black;
			font.OutlineWidth = 0.07;
			Font = font;
			backColor = color.Value;
			this.size = size;
		}

		Color backColor;
		double size;
		string text;

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
			RenderState.Translate(Center);
			RenderState.Scale(size);
			RenderState.Color = Color.White;
			Font.Render(text, 0.5, 0.5);
			RenderState.Pop();
			base.Render();
			RenderState.Pop();
		}
	}

}
