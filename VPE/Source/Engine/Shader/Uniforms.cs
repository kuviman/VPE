using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace VitPro.Engine {
	
	partial class Shader {

        Dictionary<string, int> uniformLocations = new Dictionary<string, int>();
		
		int UniformLocation(string name) {
            if (!uniformLocations.ContainsKey(name))
                uniformLocations[name] = GL.GetUniformLocation(program, name);
            return uniformLocations[name];
		}

		internal interface IUniform {
			void apply(int location, ref int textures);
		}

		internal class UniformFloat : IUniform {
			public float value;
			public UniformFloat(double value) {
				this.value = (float)value;
			}
			public void apply(int location, ref int textures) {
				GL.Uniform1(location, value);
			}
		}

        internal class UniformInt : IUniform
        {
            public int value;
            public UniformInt(int value)
            {
                this.value = value;
            }
            public void apply(int location, ref int textures)
            {
                GL.Uniform1(location, value);
            }
        }

		internal class UniformVec2 : IUniform {
			public float x, y;
			public UniformVec2(double x, double y) {
				this.x = (float)x;
				this.y = (float)y;
			}
			public void apply(int location, ref int textures) {
				GL.Uniform2(location, x, y);
			}
		}

		internal class UniformVec3 : IUniform {
			public float x, y, z;
			public UniformVec3(double x, double y, double z) {
				this.x = (float)x;
				this.y = (float)y;
				this.z = (float)z;
			}
			public void apply(int location, ref int textures) {
				GL.Uniform3(location, x, y, z);
			}
		}

		internal class UniformVec4 : IUniform {
			public float x, y, z, w;
			public UniformVec4(double x, double y, double z, double w) {
				this.x = (float)x;
				this.y = (float)y;
				this.z = (float)z;
				this.w = (float)w;
			}
			public void apply(int location, ref int textures) {
				GL.Uniform4(location, x, y, z, w);
			}
		}

		internal class UniformMat3 : IUniform {
			public Mat3 matrix;
			public UniformMat3(Mat3 matrix) {
				this.matrix = matrix;
			}
			public void apply(int location, ref int textures) {
				var mat = new OpenTK.Matrix3();
				for (int i = 0; i < 3; i++)
					for (int j = 0; j < 3; j++)
						mat[i, j] = (float)matrix[i, j];
				GL.UniformMatrix3(location, false, ref mat);
			}
		}

		internal class UniformMat4 : IUniform {
			public Mat4 matrix;
			public UniformMat4(Mat4 matrix) {
				this.matrix = matrix;
			}
			public void apply(int location, ref int textures) {
				var mat = new OpenTK.Matrix4();
				for (int i = 0; i < 4; i++)
					for (int j = 0; j < 4; j++)
						mat[i, j] = (float)matrix[i, j];
				GL.UniformMatrix4(location, false, ref mat);
			}
		}

		internal class UniformTexture : IUniform {
			public Texture texture;
			public UniformTexture(Texture texture) {
				this.texture = texture;
			}
			public void apply(int location, ref int textures) {
				GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + textures));
				GL.BindTexture(TextureTarget.Texture2D, texture.tex);
				GL.Uniform1(location, textures);
				textures++;
			}
		}

        int prepared = -1;
        int preparedTextures = 0;
        int uniformTextures = 0;

		void ApplyUniforms() {
            uniformTextures = preparedTextures;
            foreach (var item in RenderState.uniforms) {
                if (item.Value.Peek().Item2 > prepared)
                    item.Value.Peek().Item1.apply(UniformLocation(item.Key), ref uniformTextures);
            }
		}

        internal void Unprepare() {
            prepared = -1;
            preparedTextures = 0;
        }

        public void Prepare() {
            prepared = -1;
            GL.UseProgram(program);
            ApplyUniforms();
            preparedTextures = uniformTextures;
            prepared = RenderState.version;
            RenderState.preparedShaders.Push(Tuple.Create(this, prepared));
        }

	}

}
