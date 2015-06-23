using System;

namespace VitPro.Engine {

	/// <summary>
	/// 3d camera.
	/// </summary>
	[Serializable]
	public class Camera {

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public Vec3 Position { get; set; }

		/// <summary>
		/// Gets or sets the rotation.
		/// </summary>
		/// <value>The rotation.</value>
		public double Rotation { get; set; }

		/// <summary>
		/// Gets or sets up angle.
		/// </summary>
		/// <value>Up angle.</value>
		public double UpAngle { get; set; }

		/// <summary>
		/// Gets or sets the field of view.
		/// </summary>
		/// <value>The field of view.</value>
		public double FOV { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.Camera"/> class.
		/// </summary>
		/// <param name="fov">Field of view.</param>
		public Camera(double fov) {
			FOV = fov;
		}

		/// <summary>
		/// Apply the camera view.
		/// </summary>
		public void Apply() {
			RenderState.ProjectionMatrix = new Mat4(
				OpenTK.Matrix4d.CreateTranslation(-Position.X, -Position.Y, -Position.Z)
				* OpenTK.Matrix4d.CreateRotationZ(-Rotation)
				* OpenTK.Matrix4d.CreateRotationX(-UpAngle - Math.PI / 2)
				* OpenTK.Matrix4d.Perspective(FOV, RenderState.Aspect, 0.1, 1e5));
			RenderState.DepthTest = true;
		}

	}

}
