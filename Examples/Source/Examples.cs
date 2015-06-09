using System;
using VitPro.Engine;
using UI = VitPro.Engine.UI;

namespace VitPro.Engine.Examples {
	
	class Examples : UI.State {

		static Examples() {
			Selector.Register<Box3d>();
			Selector.Register<EventListener>();
			Selector.Register<Physics>();
			Selector.Register<RandomFigures>();
			Selector.Register<Test>();
		}

		public static void Main() {
			App.Title = "VPE examples";
			App.Fullscreen = true;
			App.Run(new Examples());
		}

		public static State SelectedState { get; private set; }

		new class Manager : State.Manager {
			public Manager() : base(new Info()) {}
			Texture lastTexture = null;
			Texture currentTexture = null;
			double k = -1;
			public override void Update(double dt) {
				base.Update(dt);
				k -= dt * 5;
				Examples.SelectedState = CurrentState;
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
				Draw.Clear(Settings.BackgroundColor);
				if (CurrentState != null)
					CurrentState.Render();
				RenderState.EndTexture();
				currentTexture = new Texture(RenderState.Width, RenderState.Height);
				k = 1;
				NextState = state;
			}
		}

		class ExitButton : RoundSelectElement {
			public ExitButton() : base("×", Color.Red) {
				(Font as OutlinedFont).OutlineWidth = 0.075;
				TextSize = 40;
			}
			public override State GetState() {
				return null;
			}
		}

		static Manager manager = new Manager();

		public static void SelectState(State state) {
			manager.ChangeState(state);
		}

		public Examples() {
			Background = manager;

			bar.BackgroundColor = Color.LightGray;
			bar.BorderColor = Color.Black;
			bar.Anchor = bar.Origin = new Vec2(0, 1);
			Frame.Add(bar);

			Frame.Add(new ExitButton());
			Frame.Add(new RoundSelectElement<Info>("?"));
			Frame.Add(new RoundSelectElement<Settings>("S"));
			double x = 30;
			Frame.Visit(elem => {
				if (elem == bar)
					return;
				elem.Anchor = new Vec2(0, 1);
				elem.Offset = new Vec2(x, -30);
				x += 35;
			});

			var stateList = new UI.ElementList();
			stateList.Anchor = stateList.Origin = new Vec2(0.5, 1);
			stateList.Offset = new Vec2(0, -5);
			stateList.Spacing = 0;

			var selectButton = new UI.Button("select", () => SelectState(new Selector()), 10);
			stateList.Add(selectButton);

			var font = new OutlinedFont(Draw.Font as Font);
			font.OutlineColor = Color.Black;
			font.OutlineWidth = 0.1;
			nameLabel.Font = font;
			nameLabel.Padding = 0;
			stateList.Add(nameLabel);

			Frame.Add(stateList);

			fpsLabel.Anchor = fpsLabel.Origin = new Vec2(1, 1);
			fpsLabel.BackgroundColor = new Color(0, 0, 0, 0.5);
			Frame.Add(fpsLabel);
		}

		public override void Update(double dt) {
			base.Update(dt);
			Zoom = Settings.ZoomUI;
            fpsLabel.Text = "FPS: " + ((int)App.FPS).ToString();
			if (Background.Closed)
				Close();
			if (SelectedState != null)
				nameLabel.Text = SelectedState.GetType().Name;
			bar.Size = new Vec2(Frame.Size.X, 60);
		}

		UI.Element bar = new UI.Element();

		UI.Label nameLabel = new UI.Label("", 30);
		UI.Label fpsLabel = new UI.Label("", 16);

	}
}
