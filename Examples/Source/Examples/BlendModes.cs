using System;

namespace VitPro.Engine.Examples {

	class BlendModes : State {
		BlendMode blendMode = BlendMode.Default;

		public override void KeyDown(Key key) {
			base.KeyDown(key);
			if (key == Key.Number1)
				blendMode = BlendMode.Default;
			else if (key == Key.Number2)
				blendMode = BlendMode.Add;
		}

		public override void Render() {
			base.Render();
			Draw.Clear(Settings.BackgroundColor);
			RenderState.Push();
			RenderState.View2d(5);
			RenderState.Color = Color.Black;
			Draw.Rect(-2, -2, 2, 2);
			RenderState.Translate(0, -0.5);
			RenderState.BlendMode = blendMode;
			RenderState.Color = new Color(1, 0, 0, 0.5);
			Draw.Circle(0, 1, 1);
			RenderState.Color = new Color(0, 1, 0, 0.5);
			Draw.Circle(0.5, 0, 1);
			RenderState.Color = new Color(0, 0, 1, 0.5);
			Draw.Circle(-0.5, 0, 1);
			RenderState.Pop();
		}
	}

}
