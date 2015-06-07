using System;

namespace VitPro.Engine.Examples {

	class Box3d : State {

		static Texture texture = new Texture(Resource.Stream("box.png"));

		double ax = 0.5;
		double ay = 0.1;

		Vec2? prevPos = null;

		double sens = 0.01;

		public override void MouseMove(Vec2 position) {
			base.MouseMove(position);
			if (MouseButton.Left.Pressed()) {
				if (prevPos.HasValue) {
					ax += sens * (position - prevPos.Value).X;
					ay -= sens * (position - prevPos.Value).Y;
				}
				prevPos = position;
			}
		}

		public override void MouseUp(MouseButton button, Vec2 position) {
			base.MouseUp(button, position);
			prevPos = null;
		}

		public override void Render() {
			base.Render();
			Draw.Clear(Settings.BackgroundColor);
			RenderState.Push();
			RenderState.ViewPerspective(Math.PI / 2);
			RenderState.DepthTest = true;
			RenderState.Translate(0, 0, -5);
			RenderState.RotateY(ax);
			RenderState.RotateX(ay);
			RenderState.Scale(3);
			RenderState.Origin(0.5, 0.5, 0.5);
			Draw.Box(texture);
			RenderState.Pop();
		}

	}

}
