using System;

namespace VitPro.Engine {

	/// <summary>
	/// Game state.
	/// </summary>
	public partial class State {
		
		/// <summary>
		/// Gets a value indicating whether this <see cref="VitPro.Engine.State"/> is closed.
		/// </summary>
		/// <value><c>true</c> if closed; otherwise, <c>false</c>.</value>
		public bool Closed { get; private set; }

		/// <summary>
		/// Close this <see cref="VitPro.Engine.State"/>.
		/// </summary>
		public void Close() {
			Closed = true;
		}

		/// <summary>
		/// Render this <see cref="VitPro.Engine.State"/>.
		/// </summary>
		public virtual void Render() {}

		/// <summary>
		/// Update this <see cref="VitPro.Engine.State"/>.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public virtual void Update(double dt) {}

		/// <summary>
		/// Handles key down event.
		/// </summary>
		/// <param name="key">Key pressed.</param>
		public virtual void KeyDown(Key key) {}

		/// <summary>
		/// Handles key up event.
		/// </summary>
		/// <param name="key">Key released.</param>
		public virtual void KeyUp(Key key) {}

		/// <summary>
		/// Handles character input event.
		/// </summary>
		/// <param name="c">Character input.</param>
		public virtual void CharInput(char c) {}

		/// <summary>
		/// Handles mouse button down event.
		/// </summary>
		/// <param name="button">Mouse button pressed.</param>
		/// <param name="position">Mouse position.</param>
		public virtual void MouseDown(MouseButton button, Vec2 position) {}

		/// <summary>
		/// Handles mouse button up event.
		/// </summary>
		/// <param name="button">Mouse button released.</param>
		/// <param name="position">Mouse position.</param>
		public virtual void MouseUp(MouseButton button, Vec2 position) {}

		/// <summary>
		/// Handles mouse move event.
		/// </summary>
		/// <param name="position">Mouse position.</param>
		public virtual void MouseMove(Vec2 position) {}

		/// <summary>
		/// Handles mouse wheel event.
		/// </summary>
		/// <param name="delta">Delta.</param>
		public virtual void MouseWheel(double delta) {}
	}

}
