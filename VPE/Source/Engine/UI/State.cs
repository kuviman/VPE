using System;

namespace VitPro.Engine.UI {

	/// <summary>
	/// State with UI enabled.
	/// </summary>
	public class State : VitPro.Engine.State {

		/// <summary>
		/// Gets or sets the background state.
		/// </summary>
		/// <value>The background state.</value>
		public VitPro.Engine.State Background { get; set; }

        /// <summary>
        /// Gets or sets the background color (null for no background).
        /// </summary>
        /// <value>The background color.</value>
        public Color? BackgroundColor { get; set; }

		/// <summary>
		/// Gets the main UI frame.
		/// </summary>
		/// <value>The frame.</value>
        public Frame Frame { get; private set; }

		/// <summary>
		/// Gets or sets the focused element.
		/// </summary>
		/// <value>The focused element.</value>
		public Element Focus { get; set; }

        /// <summary>
        /// Initializes a new UI state.
        /// </summary>
        public State() {
            Frame = new Frame();
            Zoom = 1;
        }

		/// <summary>
		/// Gets or sets the zoom.
		/// </summary>
		/// <value>The zoom.</value>
		public double Zoom { get; set; }

		/// <summary>
		/// Render this instance.
		/// </summary>
		public override void Render() {
			base.Render();
            if (BackgroundColor.HasValue)
                Draw.Clear(BackgroundColor.Value);
			if (Background != null)
                Background.Render();
			Frame.Size = new Vec2(RenderState.Width, RenderState.Height) / Zoom;
			Frame.Origin = Vec2.Zero;
			for (int i = 0; i < 10; i++)
				Frame.Update(0);
			RenderState.Push();
			RenderState.View2d(0, 0, Frame.Size.X, Frame.Size.Y);
			Frame.Render();
			RenderState.Pop();
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public override void Update(double dt) {
			base.Update(dt);
			if (Background != null)
                Background.Update(dt);
			Frame.Update(dt);
		}

		/// <summary>
		/// Handles mouse button down event.
		/// </summary>
		/// <param name="button">Mouse button pressed.</param>
		/// <param name="position">Mouse position.</param>
		public override void MouseDown(MouseButton button, Vec2 position) {
			base.MouseDown(button, position);
			if (Background != null)
                Background.MouseDown(button, position);
			position = position / Zoom;
			Frame.MouseDown(button, position);

			Element lastFocus = Focus;
			Frame.Visit(elem => {
				if (!elem.Focusable)
					return;
				if (elem.Inside(position))
					Focus = elem;
			});

			if (lastFocus != Focus) {
				if (lastFocus != null) {
					lastFocus.Focused = false;
					lastFocus.LoseFocus();
				}
				if (Focus != null) {
					Focus.Focused = true;
					Focus.Focus();
				}
			}
		}

		/// <summary>
		/// Handles mouse button up event.
		/// </summary>
		/// <param name="button">Mouse button released.</param>
		/// <param name="position">Mouse position.</param>
		public override void MouseUp(MouseButton button, Vec2 position) {
			base.MouseUp(button, position);
			if (Background != null)
                Background.MouseUp(button, position);
			Frame.MouseUp(button, position / Zoom);
		}

		/// <summary>
		/// Handles mouse move event.
		/// </summary>
		/// <param name="position">Mouse position.</param>
		public override void MouseMove(Vec2 position) {
			base.MouseMove(position);
			if (Background != null)
                Background.MouseMove(position);
			Frame.MouseMove(position / Zoom);
		}

		/// <summary>
		/// Handles key down event.
		/// </summary>
		/// <param name="key">Key pressed.</param>
		public override void KeyDown(Key key) {
			base.KeyDown(key);
			if (Background != null)
                Background.KeyDown(key);
			if (Focus != null)
				Focus.KeyDown(key);
		}

		/// <summary>
		/// Handles key repeat event.
		/// </summary>
		/// <param name="key">Key repeated.</param>
		public override void KeyRepeat(Key key) {
			base.KeyRepeat(key);
			if (Background != null)
				Background.KeyRepeat(key);
			if (Focus != null)
				Focus.KeyRepeat(key);
		}

		/// <summary>
		/// Handles key up event.
		/// </summary>
		/// <param name="key">Key released.</param>
		public override void KeyUp(Key key) {
			base.KeyUp(key);
			if (Background != null)
				Background.KeyUp(key);
			if (Focus != null)
				Focus.KeyUp(key);
		}

		/// <summary>
		/// Handles character input event.
		/// </summary>
		/// <param name="c">Character input.</param>
		public override void CharInput(char c) {
			base.CharInput(c);
			if (Background != null)
				Background.CharInput(c);
			if (Focus != null)
				Focus.CharInput(c);
		}

		/// <summary>
		/// Handles mouse wheel event.
		/// </summary>
		/// <param name="delta">Delta.</param>
		public override void MouseWheel(double delta) {
			base.MouseWheel(delta);
			if (Background != null)
				Background.MouseWheel(delta);
		}
		
	}
	
}
