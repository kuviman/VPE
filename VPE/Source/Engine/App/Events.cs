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

		static void InitEvents() {
			log.Info("Registering window events");
			window.RenderFrame += (sender, e) => {
				timer.Tick();
				if (State != null)
					State.Render();
				if (RenderState.version != 0)
					throw new Exception("WTF version is bad");
				FinalizeGLResources();
				window.SwapBuffers();
			};
			window.UpdateFrame += (sender, e) => {
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
				if (State != null)
					State.KeyUp((Key)e.Key);
			};
			window.KeyPress += (sender, e) => {
				if (State != null)
					State.CharInput(e.KeyChar);
			};
			window.MouseDown += (sender, e) => {
				if (State != null)
					State.MouseDown((MouseButton)e.Button, FixMouse(Mouse.Position));
			};
			window.MouseUp += (sender, e) => {
				if (State != null)
					State.MouseUp((MouseButton)e.Button, FixMouse(Mouse.Position));
			};
			window.MouseMove += (sender, e) => {
				if (State != null)
					State.MouseMove(FixMouse(Mouse.Position));
			};
			window.MouseWheel += (sender, e) => {
				if (State != null)
					State.MouseWheel(e.DeltaPrecise);
			};
            window.Resize += (sender, e) => {
                OpenTK.Graphics.OpenGL.GL.Viewport(0, 0, Width, Height);
            };
		}

		static Vec2 FixMouse(Vec2 pos) {
//			if (Multisampling)
//				return pos * multisampleFactor;
//			else
				return pos;
		}

	}

}
