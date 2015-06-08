using System;
using System.Collections.Concurrent;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace VitPro.Engine {
	
	partial class App {
		
		static void InitGL() {
			log.Info("Initializing OpenGL Context");
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		internal static ConcurrentQueue<int> garbageTextures = new ConcurrentQueue<int>();

		static void FinalizeGLResources() {
			int id;
			while (garbageTextures.TryDequeue(out id))
				GL.DeleteTexture(id);
		}

	}

}