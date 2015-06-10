using System;
using OpenTK.Graphics.OpenGL;
using log4net;
using System.Collections.Generic;

namespace VitPro.Engine {

	/// <summary>
	/// GLSL Shader.
	/// </summary>
	public partial class Shader {
		
		static ILog log = LogManager.GetLogger(typeof(Shader));

		static int vertexShader;
		static Shader() {
			App.Init();
			log.Info("Compiling basic vertex shader");
			vertexShader = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vertexShader, Resource.String("Shaders/Vertex/Basic.glsl"));
			GL.CompileShader(vertexShader);

			AddLib(Resource.String("Shaders/Lib/HSV.glsl"));
		}

		int program;

		static List<string> libSources = new List<string>();

		/// <summary>
		/// Add a lib which will be accessible from all the shaders.
		/// </summary>
		/// <param name="source">Lib source.</param>
		public static void AddLib(string source) {
			libSources.Add(source);
		}
		
		/// <summary>
		/// Compile a shader.
		/// </summary>
		/// <param name="sources">Sources.</param>
		public Shader(params string[] sources) {
			program = GL.CreateProgram();
			foreach (var source in sources) {
				var shader = GL.CreateShader(ShaderType.FragmentShader);
				var completeSource = source;
				foreach (var lib in libSources)
					completeSource = lib + completeSource;
				GL.ShaderSource(shader, completeSource);
				GL.CompileShader(shader);
				int compileStatus;
				GL.GetShader(shader, ShaderParameter.CompileStatus, out compileStatus);
				if (compileStatus == 0) {
					throw new OpenTK.GraphicsException(GL.GetShaderInfoLog(shader));
				}
				GL.AttachShader(program, shader);
			}
			GL.AttachShader(program, vertexShader);
			GL.LinkProgram(program);
			int linkStatus;
			GL.GetProgram(program, GetProgramParameterName.LinkStatus, out linkStatus);
			if (linkStatus == 0) {
				throw new OpenTK.GraphicsException(GL.GetProgramInfoLog(program));
			}
		}

		/// <summary>
		/// Renders the polygon.
		/// </summary>
		/// <param name="vertices">Vertices.</param>
		public void RenderPolygon(params Vec3[] vertices) {
			GL.UseProgram(program);
			ApplyUniforms();
			GL.Begin(PrimitiveType.Polygon);
			foreach (var vertex in vertices)
				GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
			GL.End();
		}

		/// <summary>
		/// Renders the polygon.
		/// </summary>
		/// <param name="vertices">Vertices.</param>
		public void RenderPolygon(params Vec2[] vertices) {
			GL.UseProgram(program);
			ApplyUniforms();
			GL.Begin(PrimitiveType.Polygon);
			foreach (var vertex in vertices)
				GL.Vertex2(vertex.X, vertex.Y);
			GL.End();
		}

		/// <summary>
		/// Render a quad.
		/// </summary>
		public void RenderQuad() {
			GL.UseProgram(program);
			ApplyUniforms();
			GL.Rect(0, 0, 1, 1);
		}

	}

}
