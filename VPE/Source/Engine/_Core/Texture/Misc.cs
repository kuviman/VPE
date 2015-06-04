using System;
using OpenTK.Graphics.OpenGL;

namespace VitPro.Engine {

	partial class Texture {
		
		/// <summary>
		/// Removes alpha component from the texture.
		/// </summary>
		public void RemoveAlpha() {
			RenderState.BeginTexture(this);
			GL.ColorMask(false, false, false, true);
			GL.ClearColor(0, 0, 0, 1);
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ColorMask(true, true, true, true);
			RenderState.EndTexture();
		}

		/// <summary>
		/// Apply a shader to this texture.
		/// </summary>
		/// <param name="shader">Shader.</param>
		public void ApplyShader(Shader shader) {
			var result = shader.ApplyTo(this);
			GUtil.Swap(ref this.tex, ref result.tex);
			result.Dispose();
		}

	}

}
