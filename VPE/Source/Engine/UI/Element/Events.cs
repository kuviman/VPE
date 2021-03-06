﻿using System;

namespace VitPro.Engine.UI {

	partial class Element {

		/// <summary>
		/// Checks whether a point is inside this element.
		/// </summary>
		/// <param name="pos">Position of the point.</param>
		public virtual bool Inside(Vec2 pos) {
			Vec2 a = BottomLeft, b = TopRight;
			return a.X <= pos.X && pos.X <= b.X
				&& a.Y <= pos.Y && pos.Y <= b.Y;
		}

		Element mouseFocus = null;

		/// <summary>
		/// Gets a value indicating whether this <see cref="VitPro.Engine.UI.Element"/> is pressed.
		/// </summary>
		/// <value><c>true</c> if pressed; otherwise, <c>false</c>.</value>
		public bool Pressed { get; set; }

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
			foreach (var child in children) {
				if (child.Inside(position) || mouseFocus == child)
					child.MouseUp(button, position);
			}
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
		public bool Hovered { get; set; }

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

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="VitPro.Engine.UI.Element"/> is focusable.
		/// </summary>
		/// <value><c>true</c> if focusable; otherwise, <c>false</c>.</value>
		public bool Focusable { get; set; }

		/// <summary>
		/// Gets a value indicating whether this <see cref="VitPro.Engine.UI.Element"/> is focused.
		/// </summary>
		/// <value><c>true</c> if focused; otherwise, <c>false</c>.</value>
		public bool Focused { get; set; }

		/// <summary>
		/// Occurs when element gains focus.
		/// </summary>
		public event Action OnFocus;

		/// <summary>
		/// Handles the focus gain event.
		/// </summary>
		public virtual void Focus() {
			if (OnFocus != null)
				OnFocus.Invoke();
		}

		/// <summary>
		/// Occurs when element loses focus.
		/// </summary>
		public event Action OnLoseFocus;

		/// <summary>
		/// Handles the focus loss event.
		/// </summary>
		public virtual void LoseFocus() {
			if (OnLoseFocus != null)
				OnLoseFocus.Invoke();
		}

		/// <summary>
		/// Occurs when a key is pressed.
		/// </summary>
		public event Action<Key> OnKeyDown;

		/// <summary>
		/// Handles key down event.
		/// </summary>
		/// <param name="key">Key pressed.</param>
		public virtual void KeyDown(Key key) {
			if (OnKeyDown != null)
				OnKeyDown.Invoke(key);
		}

		/// <summary>
		/// Occurs when a key is repeated.
		/// </summary>
		public event Action<Key> OnKeyRepeat;

		/// <summary>
		/// Handles key repeat event.
		/// </summary>
		/// <param name="key">Key repeated.</param>
		public virtual void KeyRepeat(Key key) {
			if (OnKeyRepeat != null)
				OnKeyRepeat.Invoke(key);
		}

		/// <summary>
		/// Occurs when a key is released.
		/// </summary>
		public event Action<Key> OnKeyUp;

		/// <summary>
		/// Handles key up event.
		/// </summary>
		/// <param name="key">Key released.</param>
		public virtual void KeyUp(Key key) {
			if (OnKeyUp != null)
				OnKeyUp.Invoke(key);
		}

		/// <summary>
		/// Occurs when on a character is input.
		/// </summary>
		public event Action<char> OnCharInput;

		/// <summary>
		/// Handles character input event.
		/// </summary>
		/// <param name="c">Character input.</param>
		public virtual void CharInput(char c) {
			if (OnCharInput != null)
				OnCharInput.Invoke(c);
		}

	}

}
