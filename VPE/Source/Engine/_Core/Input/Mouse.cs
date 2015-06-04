using System;

namespace VitPro.Engine {

	/// <summary>
	/// Provides methods for working with the mouse device.
	/// </summary>
	public static class Mouse {

		/// <summary>
		/// Gets or sets mouse position.
		/// </summary>
		/// <value>The position.</value>
		public static Vec2i Position {
			get { return new Vec2i(App.window.Mouse.X, App.Height - 1 - App.window.Mouse.Y); }
			set {
				System.Windows.Forms.Cursor.Position = App.window.PointToScreen(
					new System.Drawing.Point(value.X, App.Height - 1 - value.Y));
			}
		}

		static bool _visible = true;

		/// <summary>
		/// Gets or sets a value indicating whether mouse cursor is visible.
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		public static bool Visible {
			get { return _visible; }
			set {
				_visible = value;
				if (_visible)
					System.Windows.Forms.Cursor.Show();
				else
					System.Windows.Forms.Cursor.Hide();
			}
		}

		/// <summary>
		/// Checks whether a mouse button is currently pressed.
		/// </summary>
		/// <param name="button">Mouse button to check.</param>
		public static bool Pressed(this MouseButton button) {
			return App.window.Mouse[(OpenTK.Input.MouseButton)button];
		}

	}

}
