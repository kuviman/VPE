using System;
using OpenTK;

namespace VitPro {

	/// <summary>
	/// 4x4 matrix.
	/// </summary>
	public partial struct Mat4 {
		
		internal Matrix4d mat;
		internal Mat4(Matrix4d mat) {
			this.mat = mat;
		}

		public double this[int i, int j] {
			get { return mat[i, j]; }
			set { mat[i, j] = value; }
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="VitPro.Mat4"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="VitPro.Mat4"/>.</returns>
		public override string ToString() {
			return mat.ToString();
		}

        static Mat4 _identity = new Mat4(Matrix4d.Identity);

		/// <summary>
		/// Gets the identity matrix.
		/// </summary>
		/// <value>The identity matrix.</value>
        public static Mat4 Identity { get { return _identity; } }

	}

}