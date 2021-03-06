﻿using System;
using System.Collections.Generic;
using log4net;

namespace VitPro.Engine {

	/// <summary>
	/// Provides methods to manipulate render state.
	/// </summary>
	public static partial class RenderState {

		static RenderState() {
			App.Init();
			ClearState();
		}

		static void ClearState() {
			ProjectionMatrix = Mat4.Identity;
			ModelMatrix = Mat4.Identity;
			Color = new Color(1, 1, 1, 1);
			DepthTest = false;
			BlendMode = BlendMode.Default;
		}

		internal static Dictionary<string, Stack<Tuple<Shader.IUniform, int>>> uniforms =
			new Dictionary<string, Stack<Tuple<Shader.IUniform, int>>>();
		internal static int version = 0;

        internal static Stack<Tuple<Shader, int>> preparedShaders = new Stack<Tuple<Shader, int>>();

		/// <summary>
		/// Push (save) current render state.
		/// </summary>
		public static void Push() {
			++version;
		}

		/// <summary>
		/// Load previously pushed render state.
		/// </summary>
		public static void Pop() {
			--version;
			var toRemove = new List<string>();
			foreach (var item in uniforms) {
				while (item.Value.Count > 0 && item.Value.Peek().Item2 > version)
					item.Value.Pop();
				if (item.Value.Count == 0)
					toRemove.Add(item.Key);
			}
			foreach (var key in toRemove)
				uniforms.Remove(key);

            while (preparedShaders.Count > 0 && preparedShaders.Peek().Item2 > version) {
                preparedShaders.Pop().Item1.Unprepare();
            }
		}

		static void Set(string name, Shader.IUniform uniform) {
			if (!uniforms.ContainsKey(name))
				uniforms[name] = new Stack<Tuple<Shader.IUniform, int>>();
			uniforms[name].Push(new Tuple<Shader.IUniform, int>(uniform, version));
		}

		static T Get<T>(string name) where T : Shader.IUniform {
			return (T)uniforms[name].Peek().Item1;
		}

		/// <summary>
		/// Set a uniform for shaders.
		/// </summary>
		/// <param name="name">Uniform name.</param>
		/// <param name="value">Uniform value.</param>
		public static void Set(string name, double value) {
			Set(name, new Shader.UniformFloat(value));
		}

        /// <summary>
        /// Set a uniform for shaders.
        /// </summary>
        /// <param name="name">Uniform name.</param>
        /// <param name="value">Uniform value.</param>
        public static void Set(string name, int value)
        {
            Set(name, new Shader.UniformInt(value));
        }

		/// <summary>
		/// Set a uniform for shaders.
		/// </summary>
		/// <param name="name">Uniform name.</param>
		/// <param name="value">Uniform value.</param>
		public static void Set(string name, Vec2 value) {
			Set(name, new Shader.UniformVec2(value.X, value.Y));
		}

        /// <summary>
        /// Set a uniform for shaders.
        /// </summary>
        /// <param name="name">Uniform name.</param>
        /// <param name="value1">Uniform first value.</param>
        /// <param name="value2">Uniform second value.</param>
        public static void Set(string name, double value1, double value2)
        {
            Set(name, new Shader.UniformVec2(value1, value2));
        }

		/// <summary>
		/// Set a uniform for shaders.
		/// </summary>
		/// <param name="name">Uniform name.</param>
		/// <param name="value">Uniform value.</param>
		public static void Set(string name, Vec3 value) {
			Set(name, new Shader.UniformVec3(value.X, value.Y, value.Z));
		}

        /// <summary>
        /// Set a uniform for shaders.
        /// </summary>
        /// <param name="name">Uniform name.</param>
        /// <param name="value1">Uniform first value.</param>
        /// <param name="value2">Uniform second value.</param>
        /// <param name="value3">Uniform third value.</param>
        public static void Set(string name, double value1, double value2, double value3)
        {
            Set(name, new Shader.UniformVec3(value1, value2, value3));
        }

        /// <summary>
        /// Set a uniform for shaders.
        /// </summary>
        /// <param name="name">Uniform name.</param>
        /// <param name="value1">Uniform first value.</param>
        /// <param name="value2">Uniform second value.</param>
        /// <param name="value3">Uniform third value.</param>
        /// <param name="value4">Uniform third value.</param>
        public static void Set(string name, double value1, double value2, double value3, double value4)
        {
            Set(name, new Shader.UniformVec4(value1, value2, value3, value4));
        }

		/// <summary>
		/// Set a uniform for shaders.
		/// </summary>
		/// <param name="name">Uniform name.</param>
		/// <param name="value">Uniform value.</param>
		public static void Set(string name, Mat4 value) {
			Set(name, new Shader.UniformMat4(value));
		}

		/// <summary>
		/// Set a uniform for shaders.
		/// </summary>
		/// <param name="name">Uniform name.</param>
		/// <param name="value">Uniform value.</param>
		public static void Set(string name, Mat3 value) {
			Set(name, new Shader.UniformMat3(value));
		}

		/// <summary>
		/// Set a uniform for shaders.
		/// </summary>
		/// <param name="name">Uniform name.</param>
		/// <param name="value">Uniform value.</param>
		public static void Set(string name, Color value) {
			Set(name, new Shader.UniformVec4(value.R, value.G, value.B, value.A));
		}

		/// <summary>
		/// Set a uniform for shaders.
		/// </summary>
		/// <param name="name">Uniform name.</param>
		/// <param name="value">Uniform value.</param>
		public static void Set(string name, Texture value) {
			Set(name, new Shader.UniformTexture(value));
		}

	}

}
