using System;
using System.Collections.Generic;

namespace VitPro.Engine.Examples {

    class EventListener : State {

        Queue<string> events = new Queue<string>();

        public override void Update(double dt) {
            base.Update(dt);
        }

        public override void Render() {
            base.Render();
            int off = 40;
            int maxEvents = (int) ((RenderState.Height - off * 2 * Settings.ZoomUI) / (20 * Settings.ZoomUI));
            while (events.Count > maxEvents)
                events.Dequeue();
			Draw.Clear(Settings.BackgroundColor);
            RenderState.Push();
			RenderState.View2d(0, RenderState.Width, 0, RenderState.Height);
            RenderState.Scale(Settings.ZoomUI);
			RenderState.Translate(50, off);
			RenderState.Scale(20);
            RenderState.Translate(0, events.Count);
            RenderState.Color = Color.Black;
            foreach (var line in events) {
                RenderState.Translate(0, -1);
                Draw.Text(line);
            }
            RenderState.Pop();
        }

		public override void KeyDown(Key key) {
			base.KeyDown(key);
			events.Enqueue(string.Format("KeyDown({0})", key));
		}

		public override void KeyUp(Key key) {
			base.KeyUp(key);
			events.Enqueue(string.Format("KeyUp({0})", key));
		}

		public override void MouseDown(MouseButton button, Vec2 position) {
			base.MouseDown(button, position);
			events.Enqueue(string.Format("MouseDown({0}, {1})", button, position));
		}

		public override void MouseUp(MouseButton button, Vec2 position) {
			base.MouseUp(button, position);
			events.Enqueue(string.Format("MouseUp({0}, {1})", button, position));
		}

		public override void MouseMove(Vec2 position) {
			base.MouseMove(position);
			events.Enqueue(string.Format("MouseMove({0})", position));
		}

		public override void MouseWheel(double delta) {
			base.MouseWheel(delta);
			events.Enqueue(string.Format("MouseWheel({0})", delta));
		}

    }

}