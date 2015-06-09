using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace VitPro.Engine {
	
	partial class RenderState {

		class RenderTarget {
			public Texture texture;
			int fb;
			int depthTexture;

			public RenderTarget(Texture texture) {
				this.texture = texture;
			}

			public void Start() {
				int w = texture.Width, h = texture.Height;
				depthTexture = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D, depthTexture);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent24, w, h, 0,
					PixelFormat.DepthComponent, PixelType.UnsignedByte, IntPtr.Zero);
				fb = GL.GenFramebuffer();
				GL.BindFramebuffer(FramebufferTarget.Framebuffer, fb);
				GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
					TextureTarget.Texture2D, texture.tex, 0);
				GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment,
					TextureTarget.Texture2D, depthTexture, 0);
				if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
					throw new OpenTK.GraphicsException("Framebuffer is wrong");
				GL.BindFramebuffer(FramebufferTarget.Framebuffer, fb);
			}

			public void Finish() {
				GL.DeleteFramebuffer(fb);
				GL.DeleteTexture(depthTexture);
			}
		}

		static Stack<RenderTarget> targetStack = new Stack<RenderTarget>();

		/// <summary>
		/// Start rendering to the texture.
		/// </summary>
		/// <param name="texture">Render target.</param>
		public static void BeginTexture(Texture texture) {
			if (targetStack.Count != 0)
				targetStack.Peek().Finish();
			targetStack.Push(new RenderTarget(texture));
			targetStack.Peek().Start();

			RenderState.Push();
            RenderState.ClearState();
			areaStack.Push(null);
			setupViewport();
		}

		/// <summary>
		/// Stop rendering to a texture.
		/// </summary>
		public static void EndTexture() {
			RenderState.Pop();

			targetStack.Pop().Finish();
			if (targetStack.Count != 0)
				targetStack.Peek().Start();
			else
				GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
			areaStack.Pop();
			setupViewport();
		}

		struct RenderArea {
			public Vec2i pos, size;
		}

		static Stack<RenderArea?> areaStack = new Stack<RenderArea?>();

		static void setupViewport() {
			RenderArea area;
			if (areaStack.Count == 0 || areaStack.Peek() == null) {
				area.pos = Vec2i.Zero;
				area.size = Size;
			} else
				area = areaStack.Peek().Value;
			GL.Enable(EnableCap.ScissorTest);
			GL.Scissor(area.pos.X, area.pos.Y, area.size.X, area.size.Y);
			GL.Viewport(area.pos.X, area.pos.Y, area.size.X, area.size.Y);
		}

		/// <summary>
		/// Begin rendering to an area.
		/// </summary>
		/// <param name="pos">Bottom left corner of the area.</param>
		/// <param name="size">Size of the area.</param>
		public static void BeginArea(Vec2i pos, Vec2i size) {
			Vec2i curPos = Vec2i.Zero;
			if (areaStack.Count != 0 && areaStack.Peek() != null)
				curPos = areaStack.Peek().Value.pos;
			pos += curPos;

			size.X = Math.Max(size.X, 1);
			size.Y = Math.Max(size.Y, 1);

			var area = new RenderArea();
			area.pos = pos;
			area.size = size;
			areaStack.Push(area);
			setupViewport();
		}

		/// <summary>
		/// End rendering to an area.
		/// </summary>
		public static void EndArea() {
			areaStack.Pop();
			setupViewport();
		}

		/// <summary>
		/// Gets the size of the current render target.
		/// </summary>
		/// <value>The size.</value>
		public static Vec2i Size {
			get {
				if (areaStack.Count == 0 || areaStack.Peek() == null)
					return targetStack.Count == 0 ? App.Size : targetStack.Peek().texture.Size;
				else
					return areaStack.Peek().Value.size;
			}
		}

		/// <summary>
		/// Gets the width of the current render target.
		/// </summary>
		/// <value>The width.</value>
		public static int Width {
			get { return Size.X; }
		}

		/// <summary>
		/// Gets the height of the current render target.
		/// </summary>
		/// <value>The height.</value>
		public static int Height {
			get { return Size.Y; }
		}

		/// <summary>
		/// Gets the aspect ratio of the current render target.
		/// </summary>
		/// <value>The aspect ratio.</value>
		public static double Aspect {
			get { return (double) Width / Height; }
		}

	}
	
}
