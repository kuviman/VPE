using System;
using OpenTK.Graphics.OpenGL;

namespace VitPro.Engine {

	/// <summary>
	/// Provides basic rendering methods.
	/// </summary>
	public static partial class Draw {
		
		static Draw() {
			App.Init();
            InitText();
		}

		/// <summary>
		/// Clear the screen.
		/// </summary>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="a">The alpha component.</param>
		public static void Clear(double r, double g, double b, double a = 1) {
			GL.ClearColor((float)r, (float)g, (float)b, (float)a);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		/// <summary>
		/// Clear the screen.
		/// </summary>
		/// <param name="color">Color.</param>
		public static void Clear(Color color) {
			Clear(color.R, color.G, color.B, color.A);
		}

	}

}
