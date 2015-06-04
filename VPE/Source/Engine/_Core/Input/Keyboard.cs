using System;

namespace VitPro.Engine {

	/// <summary>
	/// Provides methods for working with the keyboard device.
	/// </summary>
	public static class Keyboard {

		/// <summary>
		/// Check if the key is currently pressed.
		/// </summary>
		/// <param name="key">Key to check.</param>
		public static bool Pressed(this Key key) {
			return App.window.Keyboard[(OpenTK.Input.Key)key];
		}

	}

}
