using System;

namespace VitPro.Engine {
	
	partial class App {

		/// <summary>
		/// Gets the main game state.
		/// </summary>
		/// <value>The state.</value>
		public static State State { get; private set; }

		internal static bool Running = true;

		/// <summary>
		/// Run using specified game state.
		/// </summary>
		/// <param name="state">Game state.</param>
		public static void Run(State state) {
			try {
				State = new State.Manager(state);
				window.Run();
			} finally {
				Running = false;
			}
		}

		/// <summary>
		/// Kill the application.
		/// </summary>
		public static void Kill() {
			window.Close();
		}

		/// <summary>
		/// Gets number of frames per second.
		/// </summary>
		/// <value>FPS.</value>
		public static double FPS { get { return timer.FPS; } }

		static Timer timer = new Timer();

		public static event Action OnRender;
		public static event Action<double> OnUpdate;
		public static event Action<Key> OnKeyDown;
		public static event Action<Key> OnKeyUp;
		public static event Action<char> OnKeyPress;
		public static event Action<MouseButton, Vec2> OnMouseDown;
		public static event Action<MouseButton, Vec2> OnMouseUp;
		public static event Action<Vec2> OnMouseMove;
		public static event Action<double> OnMouseWheel;

		static void InitEvents() {
			log.Info("Registering window events");
			window.RenderFrame += (sender, e) => {
				timer.Tick();
				OnRender.Apply();
				if (State != null)
					State.Render();
				if (RenderState.version != 0)
					throw new Exception("WTF version is bad");
				FinalizeGLResources();
				window.SwapBuffers();
			};
			window.UpdateFrame += (sender, e) => {
				OnUpdate.Apply(e.Time);
				var state = State;
				if (state != null) {
					state.Update(e.Time);
					if (state.Closed)
						State = null;
				}
				if (State == null)
					Kill();
			};
			window.KeyDown += (sender, e) => {
				OnKeyDown.Apply((Key)e.Key);
				if (State != null) {
					if (e.IsRepeat)
						State.KeyRepeat((Key)e.Key);
					else
						State.KeyDown((Key)e.Key);
				}
                if (e.Key == OpenTK.Input.Key.F4 && e.Modifiers.HasFlag(OpenTK.Input.KeyModifiers.Alt))
                    window.Close();
			};
			window.KeyUp += (sender, e) => {
				OnKeyUp.Apply((Key)e.Key);
				if (State != null)
					State.KeyUp((Key)e.Key);
			};
			window.KeyPress += (sender, e) => {
				OnKeyPress.Apply(e.KeyChar);
				if (State != null)
					State.CharInput(e.KeyChar);
			};
			window.MouseDown += (sender, e) => {
				OnMouseDown.Apply((MouseButton)e.Button, FixMouse(Mouse.Position));
				if (State != null)
					State.MouseDown((MouseButton)e.Button, FixMouse(Mouse.Position));
			};
			window.MouseUp += (sender, e) => {
				OnMouseUp.Apply((MouseButton)e.Button, FixMouse(Mouse.Position));
				if (State != null)
					State.MouseUp((MouseButton)e.Button, FixMouse(Mouse.Position));
			};
			window.MouseMove += (sender, e) => {
				OnMouseMove.Apply(FixMouse(Mouse.Position));
				if (State != null)
					State.MouseMove(FixMouse(Mouse.Position));
			};
			window.MouseWheel += (sender, e) => {
				OnMouseWheel.Apply(e.DeltaPrecise);
				if (State != null)
					State.MouseWheel(e.DeltaPrecise);
			};
            window.Resize += (sender, e) => {
                OpenTK.Graphics.OpenGL.GL.Viewport(0, 0, Width, Height);
            };
		}

		static Vec2 FixMouse(Vec2 pos) {
			return pos;
		}

	}

}
