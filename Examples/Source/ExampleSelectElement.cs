using System;

namespace VitPro.Engine.Examples {

	abstract class ExampleSelectElement : UI.Element {

		public ExampleSelectElement() {}

		public bool StateSelected {
			get {
				State current = Examples.SelectedState;
				State mine = GetState();
				if (current == null || mine == null)
					return false;
				return current.GetType() == mine.GetType();
			}
		}

		public abstract State GetState();

		public override void Click() {
			base.Click();
			Examples.SelectState(GetState());
		}

	}

	class ExampleSelectElement<S> : ExampleSelectElement where S : State, new() {
		public override State GetState() {
			return new S();
		}
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
		double t = 0;

		public override void Update(double dt) {
			base.Update(dt);
			t += 10 * dt;
		}

		public override void Render() {
			RenderState.Push();
			if (Pressed)
				RenderState.Translate(1, -2);
			RenderState.Push();
			if (StateSelected) {
				RenderState.Color = new Color(1, 1, 0, 0.5);
				Draw.Circle(Center, Math.Max(Size.X, Size.Y) / 1.7 + 1 + (1 + Math.Sin(t)) * 1);
			}
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

	class RoundSelectElement<S> : RoundSelectElement where S : State, new() {
		public RoundSelectElement(string text, Color? color = null, double size = 30) : base(text, color, size) {}
		public override State GetState() {
			return new S();
		}
	}

}
