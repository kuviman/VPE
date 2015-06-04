﻿using System;

namespace VitPro.Engine {
	
	partial class Shader {

		/// <summary>
		/// Standard shaders.
		/// </summary>
		public static class Std {

            static Std() {
                Color = Load("ColorShader");
                Circle = Load("CircleShader");
                Texture = Load("TextureShader");
            }
			
			static Shader Load(string name) {
				log.Info("Loading " + name);
				return new Shader(Resource.String(name));
			}

			/// <summary>
			/// Basic coloring shader.
			/// </summary>
			/// <value>Color shader.</value>
            public static Shader Color { get; private set; }

			/// <summary>
			/// Circle shader.
			/// </summary>
			/// <value>Circle shader.</value>
            public static Shader Circle { get; private set; }

			/// <summary>
			/// Texture shader.
			/// </summary>
			/// <value>Texture shader.</value>
            public static Shader Texture { get; private set; }

		}

	}

}
