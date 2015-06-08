using System;
using OpenTK.Graphics.OpenGL;

namespace VitPro.Engine {

	partial class RenderState {

		class Enabler : Shader.IUniform {
			public EnableCap cap;
			public bool enable;
			public Enabler(EnableCap cap, bool enable) {
				this.cap = cap;
				this.enable = enable;
			}
			public void apply(int location, ref int textures) {
				if (enable)
					GL.Enable(cap);
				else
					GL.Disable(cap);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether depth test is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public static bool DepthTest {
			get { return Get<Enabler>("enabler_DepthTest").enable; }
			set { Set("enabler_DepthTest", new Enabler(EnableCap.DepthTest, value)); }
		}

		/// <summary>
		/// Gets or sets the rendering color.
		/// </summary>
		/// <value>The color.</value>
		public static Color Color {
			get {
				var uniform = Get<Shader.UniformVec4>("color");
				return new Color(uniform.x, uniform.y, uniform.z, uniform.w);
			}
			set { Set("color", new Shader.UniformVec4(value.R, value.G, value.B, value.A)); }
		}

	}

}
