using System;
using OpenTK.Graphics.OpenGL;
using log4net;

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
			GL.ShaderSource(vertexShader, Resource.String("VertexShader"));
			GL.CompileShader(vertexShader);
		}

		int program;
		
		/// <summary>
		/// Compile a shader.
		/// </summary>
		/// <param name="sources">Sources.</param>
		public Shader(params string[] sources) {
			program = GL.CreateProgram();
			foreach (var source in sources) {
				var shader = GL.CreateShader(ShaderType.FragmentShader);
				GL.ShaderSource(shader, source);
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
			RenderPolygon(new Vec2(0, 0), new Vec2(1, 0), new Vec2(1, 1), new Vec2(0, 1));
		}

	}

}
