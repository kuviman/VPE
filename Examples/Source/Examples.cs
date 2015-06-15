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
			Selector.Register<BlendModes>();
			Selector.Register<Pong>();
		}

		public static void Main() {
			App.Title = "VPE examples";
			App.Fullscreen = true;
//			Mouse.Visible = false;
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

			Vec2i size = new Vec2i(1, 1);

			public override void Render() {
				size = RenderState.Size;
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
				lastTexture = new Texture(size.X, size.Y);
				RenderState.BeginTexture(lastTexture);
				Draw.Clear(Settings.BackgroundColor);
				if (CurrentState != null)
					CurrentState.Render();
				RenderState.EndTexture();
				currentTexture = new Texture(size.X, size.Y);
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
			if (state != null)
				nameLabel.Text = state.GetType().Name;
			else
				manager.Close();
		}

		public Examples() {
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

			stateFrame = new UI.StateFrame(manager);
			stateFrame.Anchor = stateFrame.Origin = new Vec2(0, 0);
			Frame.Add(stateFrame);

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

			Focus = stateFrame;

			SelectState(new Info());
		}

		public override void Update(double dt) {
			base.Update(dt);
			Zoom = Settings.ZoomUI;
            fpsLabel.Text = "FPS: " + ((int)App.FPS).ToString();
			if (manager.Closed)
				Close();
			bar.Size = new Vec2(Frame.Size.X, 60);
			stateFrame.Size = new Vec2(Frame.Size.X, Frame.Size.Y - 60);
		}

//		Texture cursor = new Texture(Resource.Stream("cursor.png"));

//		Vec2 mousePos;
//		public override void MouseMove(Vec2 position) {
//			base.MouseMove(position);
//			mousePos = position;
//		}

		public override void Render() {
			base.Render();
//			RenderState.Push();
//			RenderState.View2d(0, Frame.Size.X, 0, Frame.Size.Y);
//			RenderState.Translate(mousePos / Zoom);
//			RenderState.Scale(20);
//			RenderState.Origin(0, 1);
//			cursor.Render();
//			RenderState.Pop();
		}

		UI.Element bar = new UI.Element();

		static UI.Label nameLabel = new UI.Label("", 30);
		UI.Label fpsLabel = new UI.Label("", 16);

		UI.StateFrame stateFrame;

	}
}
