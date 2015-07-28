using System;

namespace VitPro.Engine {

	partial class Shader {

		/// <summary>
		/// Apply this shader to a texture
		/// </summary>
		/// <returns>Result.</returns>
		/// <param name="texture">Texture.</param>
		public Texture ApplyTo(Texture texture) {
			Texture tex = texture.Copy();
			Color color = RenderState.Color;
			RenderState.BeginTexture(tex);
			RenderState.Color = color;
			RenderState.Set("texture", texture);
			Draw.Clear(Color.TransparentBlack);
			RenderState.View2d(0, 0, 1, 1);
			RenderQuad();
			RenderState.EndTexture();
			return tex;
		}

	}

}
