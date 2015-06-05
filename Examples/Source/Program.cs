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

		class ExitButton : RoundSelectElement {
			public ExitButton() : base("×", Color.Red) {
				(Font as OutlinedFont).OutlineWidth = 0.075;
				TextSize = 40;
				Padding = -0.15;
			}
			public override State GetState() {
				return null;
			}
		}

		public Examples() {
			Background = manager;

			fpsLabel.Anchor = fpsLabel.Origin = new Vec2(1, 1);
			fpsLabel.BackgroundColor = new Color(0, 0, 0, 0.5);
			Frame.Add(fpsLabel);

			Frame.Add(new ExitButton());
			Frame.Add(InfoState.GetSelectElement());
			Frame.Add(Settings.GetSelectElement());
			double x = 30;
			Frame.Visit(elem => {
				var a = elem as ExampleSelectElement;
				if (a == null) return;
				a.Anchor = new Vec2(0, 1);
				a.Offset = new Vec2(x, -30);
				x += 35;
			});

			var list = new UI.ElementList();
			list.Horizontal = true;
			var size = 20;
			list.Add(new UI.Button("Test", () => manager.ChangeState(new Test()), size));
			list.Add(new UI.Button("RandomFigures", () => manager.ChangeState(new RandomFigures()), size));
			list.Add(new UI.Button("Physics", () => manager.ChangeState(new Physics()), size));
			list.Add(new UI.Button("EventListener", () => manager.ChangeState(new EventListener()), size));
			list.Anchor = list.Origin = new Vec2(0.5, 0);
			list.Offset = new Vec2(0, 10);
			Frame.Add(list);

			Frame.Visit(elem => {
				if (elem is ExampleSelectElement)
					elem.OnClick += () => manager.ChangeState((elem as ExampleSelectElement).GetState());
			});
		}

		public override void Update(double dt) {
			base.Update(dt);
			Zoom = Settings.ZoomUI;
            fpsLabel.Text = "FPS: " + ((int)App.FPS).ToString();
			if (Background.Closed)
				Close();
		}

		UI.Label fpsLabel = new UI.Label("", 16);

	}
}
