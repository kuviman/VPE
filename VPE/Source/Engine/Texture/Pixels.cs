using System;
using OpenTK.Graphics.OpenGL;

namespace VitPro.Engine {

	partial class Texture {
		
		[Serializable]
		struct ColorStruct {
			public byte r, g, b, a;
			public ColorStruct(Color c) {
				r = (byte)(c.R * 255);
				g = (byte)(c.G * 255);
				b = (byte)(c.B * 255);
				a = (byte)(c.A * 255);
			}
		}

		public Color this[int x, int y] {
			get {
				ColorStruct[,] pixels = new ColorStruct[Height, Width];
				GL.BindTexture(TextureTarget.Texture2D, tex);
				GL.GetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
				var c = pixels[y, x];
				return new Color(c.r / 255.0, c.g / 255.0, c.b / 255.0, c.a / 255.0);
			}
			set {
				ColorStruct[,] pixels = new ColorStruct[1, 1];
				pixels[0, 0] = new ColorStruct(value);
				GL.BindTexture(TextureTarget.Texture2D, tex);
				GL.TexSubImage2D(TextureTarget.Texture2D, 0, x, y, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
			}
		}

	}

}
