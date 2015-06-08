using System;
using OpenTK.Graphics.OpenGL;
using log4net;

namespace VitPro.Engine {

	/// <summary>
	/// Texture.
	/// </summary>
	public partial class Texture : IDisposable {

		static ILog log = LogManager.GetLogger(typeof(Texture));

		static Texture() {
			App.Init();
		}

		internal int tex;

		internal Texture() {
			tex = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, tex);
			Smooth = DefaultSmooth;
			Wrap = WrapMode.Clamp;
		}

		~Texture() {
			Dispose();
		}

		bool disposed = false;
		public void Dispose() {
			if (disposed || !App.Running)
				return;
			App.garbageTextures.Enqueue(tex);
			disposed = true;
		}

		/// <summary>
		/// Initializes a new <see cref="VitPro.Engine.Texture"/>.
		/// </summary>
		/// <param name="width">Texture width.</param>
		/// <param name="height">Texture height.</param>
		public Texture(int width, int height) : this() {
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
				width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
		}

		/// <summary>
		/// Gets or sets a value indicating whether new textures will be smoothed or not.
		/// </summary>
		/// <value><c>true</c> if smooth; otherwise, <c>false</c>.</value>
		public static bool DefaultSmooth { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="VitPro.Engine.Texture"/> is smooth.
		/// </summary>
		/// <value><c>true</c> if smooth; otherwise, <c>false</c>.</value>
		public bool Smooth {
			get {
				GL.BindTexture(TextureTarget.Texture2D, tex);
				int filter;
				GL.GetTexParameter(TextureTarget.Texture2D, GetTextureParameter.TextureMinFilter, out filter);
				return (TextureMinFilter)filter == TextureMinFilter.Linear;
			}
			set {
				GL.BindTexture(TextureTarget.Texture2D, tex);
				var minFilter = value ? TextureMinFilter.Linear : TextureMinFilter.Nearest;
				var magFilter = value ? TextureMagFilter.Linear : TextureMagFilter.Nearest;
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);
			}
		}

		/// <summary>
		/// Gets or sets the wrap mode.
		/// </summary>
		/// <value>The wrap mode.</value>
		public WrapMode Wrap {
			get {
				GL.BindTexture(TextureTarget.Texture2D, tex);
				int mode;
				GL.GetTexParameter(TextureTarget.Texture2D, GetTextureParameter.TextureWrapS, out mode);
				return (WrapMode)mode;
			}
			set {
				GL.BindTexture(TextureTarget.Texture2D, tex);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)value);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)value);
			}
		}

		/// <summary>
		/// Texture wrap mode.
		/// </summary>
		public enum WrapMode {
			Clamp = TextureWrapMode.ClampToBorder,
			Repeat = TextureWrapMode.Repeat
		}

		/// <summary>
		/// Gets the width of the texture.
		/// </summary>
		/// <value>The width.</value>
		public int Width {
			get {
				int value;
				GL.BindTexture(TextureTarget.Texture2D, tex);
				GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out value);
				return value;
			}
		}

		/// <summary>
		/// Gets the height of the texture.
		/// </summary>
		/// <value>The height.</value>
		public int Height {
			get {
				int value;
				GL.BindTexture(TextureTarget.Texture2D, tex);
				GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out value);
				return value;
			}
		}

		/// <summary>
		/// Gets the size of the texture.
		/// </summary>
		/// <value>The size.</value>
		public Vec2i Size { get { return new Vec2i(Width, Height); } }

		/// <summary>
		/// Render a quad using this <see cref="VitPro.Engine.Texture"/>.
		/// </summary>
		public void Render() {
			RenderState.Push();
			RenderState.Set("texture", this);
			Shader.Std.Texture.RenderQuad();
			RenderState.Pop();
		}
		
	}

}
