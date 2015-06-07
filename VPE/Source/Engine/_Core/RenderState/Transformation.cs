using System;

namespace VitPro.Engine {

	partial class RenderState {

		/// <summary>
		/// Gets or sets the model matrix.
		/// </summary>
		/// <value>The model matrix.</value>
		public static Mat4 ModelMatrix {
			get { return Get<Shader.UniformMat4>("modelMatrix").matrix; }
			set { Set("modelMatrix", value); }
		}

		/// <summary>
		/// Multiplies current model matrix by the matrix.
		/// </summary>
		/// <param name="matrix">The matrix.</param>
		public static void MultMatrix(Mat4 matrix) {
			ModelMatrix = matrix * ModelMatrix;
		}

		/// <summary>
		/// Scale the model.
		/// </summary>
		/// <param name="kx">Scale factor of x coordinate.</param>
		/// <param name="ky">Scale factor of y coordinate.</param>
		/// <param name="kz">Scale factor of z coordinate.</param>
		public static void Scale(double kx, double ky, double kz) {
			MultMatrix(new Mat4(OpenTK.Matrix4d.Scale(kx, ky, kz)));
		}

		/// <summary>
		/// Scale the model.
		/// </summary>
		/// <param name="kx">Scale factor of x coordinate.</param>
		/// <param name="ky">Scale factor of y coordinate.</param>
		public static void Scale(double kx, double ky) {
			Scale(kx, ky, 1);
		}

		/// <summary>
		/// Scale the model.
		/// </summary>
		/// <param name="k">Scale factor for x and y coordinates.</param>
		public static void Scale(Vec2 k) {
			Scale(k.X, k.X, 1);
		}

		/// <summary>
		/// Scale the model.
		/// </summary>
		/// <param name="k">Scale factor for x, y and z coordinates.</param>
		public static void Scale(Vec3 k) {
			Scale(k.X, k.X, k.Z);
		}

		/// <summary>
		/// Scale the model.
		/// </summary>
		/// <param name="k">Scale factor.</param>
		public static void Scale(double k) {
			Scale(k, k, k);
		}

		/// <summary>
		/// Translate the model.
		/// </summary>
		/// <param name="dx">Translation along x axis.</param>
		/// <param name="dy">Translation along y axis.</param>
		/// <param name="dz">Translation along z axis.</param>
		public static void Translate(double dx, double dy, double dz) {
			MultMatrix(new Mat4(OpenTK.Matrix4d.CreateTranslation(dx, dy, dz)));
		}

		/// <summary>
		/// Translate the model.
		/// </summary>
		/// <param name="dx">Translation along x axis.</param>
		/// <param name="dy">Translation along y axis.</param>
		public static void Translate(double dx, double dy) {
			Translate(dx, dy, 0);
		}

		/// <summary>
		/// Translate the model.
		/// </summary>
		/// <param name="dv">Translation vector.</param>
		public static void Translate(Vec2 dv) {
			Translate(dv.X, dv.Y);
		}

		/// <summary>
		/// Translate the model.
		/// </summary>
		/// <param name="dv">Translation vector.</param>
		public static void Translate(Vec3 dv) {
			Translate(dv.X, dv.Y, dv.Z);
		}

		/// <summary>
		/// Setup model origin point.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="z">The z coordinate.</param>
		public static void Origin(double x, double y, double z) {
			Translate(-x, -y, -z);
		}

		/// <summary>
		/// Setup model origin point.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public static void Origin(double x, double y) {
			Translate(-x, -y);
		}
		/// <summary>
		/// Setup model origin point.
		/// </summary>
		/// <param name="origin">Origin.</param>
		public static void Origin(Vec2 origin) {
			Origin(origin.X, origin.Y);
		}

		/// <summary>
		/// Rotate the model.
		/// </summary>
		/// <param name="ax">X coordinate of the rotation axis.</param>
		/// <param name="ay">Y coordinate of the rotation axis.</param>
		/// <param name="az">Z coordinate of the rotation axis.</param>
		/// <param name="angle">Rotation angle.</param>
		public static void Rotate(double ax, double ay, double az, double angle) {
			MultMatrix(new Mat4(OpenTK.Matrix4d.Rotate(
				new OpenTK.Vector3d(ax, ay, az), angle)));
		}

		/// <summary>
		/// Rotates the model about x axis.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		public static void RotateX(double angle) {
			Rotate(1, 0, 0, angle);
		}

		/// <summary>
		/// Rotates the model about y axis.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		public static void RotateY(double angle) {
			Rotate(0, 1, 0, angle);
		}

		/// <summary>
		/// Rotates the model about z axis.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		public static void RotateZ(double angle) {
			Rotate(0, 0, 1, angle);
		}

		/// <summary>
		/// Rotates the model.
		/// </summary>
		/// <param name="angle">Rotation angle.</param>
		public static void Rotate(double angle) {
			RotateZ(angle);
		}

		/// <summary>
		/// Sets the orts.
		/// </summary>
		/// <param name="e1">First ort vector.</param>
		/// <param name="e2">Second ort vector.</param>
		/// <param name="center">Center.</param>
		public static void SetOrts(Vec2 e1, Vec2 e2, Vec2 center) {
			var matrix = Mat4.Identity;
			matrix[0, 0] = e1.X;
			matrix[0, 1] = e1.Y;
			matrix[1, 0] = e2.X;
			matrix[1, 1] = e2.Y;
			matrix[3, 0] = center.X;
			matrix[3, 1] = center.Y;
			MultMatrix(matrix);
		}
		
	}

}
