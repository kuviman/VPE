using System;

namespace VitPro.Engine {
	
	/// <summary>
	/// Enumerates all possible mouse buttons.
	/// </summary>
	
	public enum MouseButton {
		
		/// <summary>
		/// The left mouse button.
		/// </summary>
		Left = OpenTK.Input.MouseButton.Left,

		/// <summary>
		/// The middle mouse button.
		/// </summary>
		Middle = OpenTK.Input.MouseButton.Middle,

		/// <summary>
		/// The right mouse button.
		/// </summary>
		Right = OpenTK.Input.MouseButton.Right,

		/// <summary>
		/// Indicates the last available mouse button.
		/// </summary>
		LastButton = OpenTK.Input.MouseButton.LastButton

	}

}
