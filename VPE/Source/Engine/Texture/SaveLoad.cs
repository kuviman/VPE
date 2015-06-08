using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Reflection;

namespace VitPro.Engine {

	partial class Texture {

		/// <summary>
		/// Load a texture from a stream.
		/// </summary>
		/// <param name="stream">Stream.</param>
		public Texture(System.IO.Stream stream) : this() {
			SetBitmap(new Bitmap(stream));
		}

		/// <summary>
		/// Load a texture from a file.
		/// </summary>
		/// <param name="path">File.</param>
		public Texture(string path) : this() {
			SetBitmap(new Bitmap(path));
		}

		/// <summary>
		/// Load texture from a resource.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="name">Resource name.</param>
		public static Texture FromResource(string name) {
			log.Info(string.Format("Loading texture \"{0}\"", name));
			return new Texture(Assembly.GetCallingAssembly().GetManifestResourceStream(name));
		}

		internal void SetBitmap(Bitmap bitmap) {
			bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

			var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.BindTexture(TextureTarget.Texture2D, tex);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
				bitmap.Width, bitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			bitmap.UnlockBits(data);

			bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
		}

		/// <summary>
		/// Save the texture to a file.
		/// </summary>
		/// <param name="path">File to save the texture to.</param>
		public void Save(string path) {
			ToBitmap().Save(path);
		}

		internal Bitmap ToBitmap() {
			var bitmap = new Bitmap(Width, Height);
			GL.BindTexture(TextureTarget.Texture2D, tex);
			var data = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
				System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.GetTexImage(TextureTarget.Texture2D, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			bitmap.UnlockBits(data);

			bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
			return bitmap;
		}

		/// <summary>
		/// Copy this texture.
		/// </summary>
		public Texture Copy() {
			var tex = new Texture();
			tex.Smooth = this.Smooth;
			tex.SetBitmap(this.ToBitmap());
			return tex;
		}

	}

}
