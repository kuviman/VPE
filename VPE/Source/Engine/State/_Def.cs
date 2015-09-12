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

        public event Action OnClose;

		/// <summary>
		/// Close this <see cref="VitPro.Engine.State"/>.
		/// </summary>
		public virtual void Close() {
            if (!Closed)
                OnClose.Apply();
			Closed = true;
		}

        public event Action OnRender;

		/// <summary>
		/// Render this <see cref="VitPro.Engine.State"/>.
		/// </summary>
		public virtual void Render() {
            OnRender.Apply();
        }

        public event Action<double> OnUpdate;

		/// <summary>
		/// Update this <see cref="VitPro.Engine.State"/>.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public virtual void Update(double dt) {
            OnUpdate.Apply(dt);
        }

        public event Action<Key> OnKeyDown;

		/// <summary>
		/// Handles key down event.
		/// </summary>
		/// <param name="key">Key pressed.</param>
		public virtual void KeyDown(Key key) {
            OnKeyDown.Apply(key);
        }

        public event Action<Key> OnKeyRepeat;

		/// <summary>
		/// Handles key repeat event.
		/// </summary>
		/// <param name="key">Key repeated.</param>
		public virtual void KeyRepeat(Key key) {
            OnKeyRepeat.Apply(key);
        }

        public event Action<Key> OnKeyUp;

		/// <summary>
		/// Handles key up event.
		/// </summary>
		/// <param name="key">Key released.</param>
		public virtual void KeyUp(Key key) {
            OnKeyUp.Apply(key);
        }

        public event Action<char> OnCharInput;

		/// <summary>
		/// Handles character input event.
		/// </summary>
		/// <param name="c">Character input.</param>
		public virtual void CharInput(char c) {
            OnCharInput.Apply(c);
        }

        public event Action<MouseButton, Vec2> OnMouseDown;

		/// <summary>
		/// Handles mouse button down event.
		/// </summary>
		/// <param name="button">Mouse button pressed.</param>
		/// <param name="position">Mouse position.</param>
		public virtual void MouseDown(MouseButton button, Vec2 position) {
            OnMouseDown.Apply(button, position);
        }

        public event Action<MouseButton, Vec2> OnMouseUp;

		/// <summary>
		/// Handles mouse button up event.
		/// </summary>
		/// <param name="button">Mouse button released.</param>
		/// <param name="position">Mouse position.</param>
		public virtual void MouseUp(MouseButton button, Vec2 position) {
            OnMouseUp.Apply(button, position);
        }

        public event Action<Vec2> OnMouseMove;

		/// <summary>
		/// Handles mouse move event.
		/// </summary>
		/// <param name="position">Mouse position.</param>
		public virtual void MouseMove(Vec2 position) {
            OnMouseMove.Apply(position);
        }

        public event Action<double> OnMouseWheel;

		/// <summary>
		/// Handles mouse wheel event.
		/// </summary>
		/// <param name="delta">Delta.</param>
		public virtual void MouseWheel(double delta) {
            OnMouseWheel.Apply(delta);
        }
	}

}
