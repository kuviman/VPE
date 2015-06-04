using System;
using VitPro.Engine;
using UI = VitPro.Engine.UI;

namespace VitPro.Engine.Examples {
	
	class Examples : UI.State {

		public static void Main() {
			App.Title = "VPE examples";
			App.Run(new Examples());
		}

		new class Manager : State.Manager {
			public Manager() : base(new InfoState()) {}
			Texture lastTexture = null;
			Texture currentTexture = null;
			double k = -1;
			public override void Update(double dt) {
				base.Update(dt);
				k -= dt * 5;
			}
			public override void Render() {
				if (k > 0) {
					RenderState.BeginTexture(currentTexture);
					base.Render();
					RenderState.EndTexture();
					RenderState.Push();
					RenderState.View2d(0, 1, 0, 1);
					RenderState.Translate(k, 0);
					currentTexture.Render();
					RenderState.Translate(-1, 0);
					lastTexture.Render();
					RenderState.Pop();
				} else
					base.Render();
			}
			public void ChangeState(State state) {
				lastTexture = new Texture(RenderState.Width, RenderState.Height);
				RenderState.BeginTexture(lastTexture);
				Draw.Clear(0.8, 0.8, 1);
				if (CurrentState != null)
					CurrentState.Render();
				RenderState.EndTexture();
				currentTexture = new Texture(RenderState.Width, RenderState.Height);
				k = 1;
				NextState = state;
			}
		}

		Manager manager = new Manager();

		public Examples() {
			Background = manager;
			fpsLabel.Anchor = fpsLabel.Origin = new Vec2(1, 1);
			fpsLabel.BackgroundColor = new Color(0, 0, 0, 0.5);
			Frame.Add(fpsLabel);

			var list = new UI.ElementList();
			list.Horizontal = true;
			var size = 20;
			list.Add(new UI.Button("Info", () => manager.ChangeState(new InfoState()), size));
			list.Add(new UI.Button("Test", () => manager.ChangeState(new Test()), size));
			list.Add(new UI.Button("RandomFigures", () => manager.ChangeState(new RandomFigures()), size));
			list.Add(new UI.Button("Physics", () => manager.ChangeState(new Physics()), size));
			list.Add(new UI.Button("EventListener", () => manager.ChangeState(new EventListener()), size));
			list.Add(new UI.Button("Settings", () => manager.ChangeState(new Settings()), size));
			list.Anchor = list.Origin = new Vec2(0.5, 0);
			list.Offset = new Vec2(0, 10);

			var quitButton = new UI.Button("Quit", Close, size);
			quitButton.Anchor = quitButton.Origin = new Vec2(0, 1);
			quitButton.Offset = new Vec2(10, -10);

			Frame.Add(quitButton);
			Frame.Add(list);
		}

		public override void Update(double dt) {
			base.Update(dt);
			Zoom = Settings.ZoomUI;
            fpsLabel.Text = "FPS: " + ((int)App.FPS).ToString();
		}

		UI.Label fpsLabel = new UI.Label("", 16);

	}
}
