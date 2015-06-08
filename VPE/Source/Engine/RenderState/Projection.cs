using System;
using OpenTK.Graphics.OpenGL;

namespace VitPro.Engine {

	partial class RenderState {

		/// <summary>
		/// Gets or sets the projection matrix.
		/// </summary>
		/// <value>The projection matrix.</value>
		public static Mat4 ProjectionMatrix {
			get { return Get<Shader.UniformMat4>("projectionMatrix").matrix; }
			set { Set("projectionMatrix", value); }
		}

		/// <summary>
		/// Setup a 2d view.
		/// </summary>
		/// <param name="left">Coordinate of the left side of the screen.</param>
		/// <param name="right">Coordinate of the right side of the screen.</param>
		/// <param name="bottom">Coordinate of the bottom side of the screen.</param>
		/// <param name="top">Coordinate of the top side of the screen.</param>
		public static void View2d(double left, double right, double bottom, double top) {
			ProjectionMatrix = new Mat4(OpenTK.Matrix4d.CreateOrthographicOffCenter(left, right, bottom, top, -1, 1));
		}

		/// <summary>
		/// Setup a 2d view, looking at (0, 0).
		/// </summary>
		/// <param name="fov">Distance between the bottom and top sides of the screen.</param>
		public static void View2d(double fov) {
			double h = fov / 2;
			double w = h * RenderState.Aspect;
			View2d(-w, w, -h, h);
		}

		/// <summary>
		/// Setup perspective view.
		/// </summary>
		/// <param name="fov">Field of view.</param>
		public static void ViewPerspective(double fov) {
			ProjectionMatrix = new Mat4(OpenTK.Matrix4d.Perspective(fov, RenderState.Aspect, 0.1, 1e5));
		}

        internal static double FontSize {
            get {
                return Height * (ProjectionMatrix * ModelMatrix)[1, 1] / 2;
            }
        }

	}

}
