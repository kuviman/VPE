using System;

namespace VitPro.Engine.UI {

	partial class Element {

		/// <summary>
		/// Checks whether a point is inside this element.
		/// </summary>
		/// <param name="pos">Position of the point.</param>
		public bool Inside(Vec2 pos) {
			Vec2 a = BottomLeft, b = TopRight;
			return a.X <= pos.X && pos.X <= b.X
				&& a.Y <= pos.Y && pos.Y <= b.Y;
		}

		Element mouseFocus = null;

		/// <summary>
		/// Gets a value indicating whether this <see cref="VitPro.Engine.UI.Element"/> is pressed.
		/// </summary>
		/// <value><c>true</c> if pressed; otherwise, <c>false</c>.</value>
		public bool Pressed { get; private set; }

		/// <summary>
		/// Occurs when mouse button is presed.
		/// </summary>
		public event Action<MouseButton, Vec2> OnMouseDown;

		/// <summary>
		/// Handles mouse down events.
		/// </summary>
		/// <param name="button">Button pressed.</param>
		public virtual void MouseDown(MouseButton button, Vec2 position) {
			foreach (var child in children) {
				if (child.Inside(position)) {
					child.MouseDown(button, position);
					mouseFocus = child;
				}
			}
			if (!Pressed)
				Press();
			Pressed = true;
			if (OnMouseDown != null)
				OnMouseDown.Invoke(button, position);
		}

		/// <summary>
		/// Occurs when mouse button is released.
		/// </summary>
		public event Action<MouseButton, Vec2> OnMouseUp;

		/// <summary>
		/// Handles mouse up events.
		/// </summary>
		/// <param name="button">Button released.</param>
		public virtual void MouseUp(MouseButton button, Vec2 position) {
			if (mouseFocus != null)
				mouseFocus.MouseUp(button, position);
			mouseFocus = null;
			if (Pressed) {
				Release();
				if (Inside(position))
					Click();
			}
			Pressed = false;
			if (OnMouseUp != null)
				OnMouseUp.Invoke(button, position);
		}

		/// <summary>
		/// Occurs when element is pressed.
		/// </summary>
		public event Action OnPress;

		/// <summary>
		/// Handles press events.
		/// </summary>
		public virtual void Press() {
			if (OnPress != null)
				OnPress.Invoke();
		}

		/// <summary>
		/// Occurs when element is released.
		/// </summary>
		public event Action OnRelease;

		/// <summary>
		/// Handles release events.
		/// </summary>
		public virtual void Release() {
			if (OnRelease != null)
				OnRelease.Invoke();
		}

		/// <summary>
		/// Occurs when element is clicked.
		/// </summary>
		public event Action OnClick;

		/// <summary>
		/// Handles click events.
		/// </summary>
		public virtual void Click() {
			if (OnClick != null)
				OnClick.Invoke();
		}

		/// <summary>
		/// Occurs when mouse moves.
		/// </summary>
		public event Action<Vec2> OnMouseMove;

		/// <summary>
		/// Handles mouse move events.
		/// </summary>
		/// <param name="position">Mouse position.</param>
		public virtual void MouseMove(Vec2 position) {
			foreach (var child in children) {
				child.MouseMove(position);
				if (child.Inside(position)) {
					if (!child.Hovered)
						child.Hover();
					child.Hovered = true;
				} else {
					if (child.Hovered)
						child.Unhover();
					child.Hovered = false;
				}
			}
			if (OnMouseMove != null)
				OnMouseMove.Invoke(position);
		}

		/// <summary>
		/// Gets a value indicating whether mouse is hovered over this element.
		/// </summary>
		/// <value><c>true</c> if hovered; otherwise, <c>false</c>.</value>
		public bool Hovered { get; private set; }

		/// <summary>
		/// Occurs when mouse is hovered over the element.
		/// </summary>
		public event Action OnHover;

		/// <summary>
		/// Handles mouse hover events.
		/// </summary>
		public virtual void Hover() {
			if (OnHover != null)
				OnHover.Invoke();
		}

		/// <summary>
		/// Occurs when mouse leaves the element.
		/// </summary>
		public event Action OnUnhover;

		/// <summary>
		/// Handles mouse unhover events.
		/// </summary>
		public virtual void Unhover() {
			if (OnUnhover != null)
				OnUnhover.Invoke();
		}

	}

}
