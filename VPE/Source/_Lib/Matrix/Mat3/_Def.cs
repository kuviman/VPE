using System;
using OpenTK;

namespace VitPro {

	/// <summary>
	/// 3x3 matrix.
	/// </summary>
	[Serializable]
	public partial struct Mat3 {

        static Mat3() {
            Identity = new Mat3(Matrix3d.Identity);
        }
		
		internal Matrix3d mat;
		internal Mat3(Matrix3d mat) {
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

		/// <summary>
		/// Gets the identity matrix.
		/// </summary>
		/// <value>The identity matrix.</value>
        public static Mat3 Identity { get; private set; }

	}

}