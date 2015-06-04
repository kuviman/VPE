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
				GL.Viewport(0, 0, w, h);
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
		}

		/// <summary>
		/// Stop rendering to a texture.
		/// </summary>
		public static void EndTexture() {
			RenderState.Pop();

			targetStack.Pop().Finish();
			if (targetStack.Count != 0)
				targetStack.Peek().Start();
			else {
				GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
				GL.Viewport(0, 0, App.Width, App.Height);
			}
		}

		/// <summary>
		/// Gets the width of the current render target.
		/// </summary>
		/// <value>The width.</value>
		public static int Width {
			get { return targetStack.Count == 0 ? App.Width : targetStack.Peek().texture.Width;	}
		}

		/// <summary>
		/// Gets the height of the current render target.
		/// </summary>
		/// <value>The height.</value>
		public static int Height {
			get { return targetStack.Count == 0 ? App.Height : targetStack.Peek().texture.Height;	}
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
