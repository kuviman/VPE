using System;
using OpenTK;

namespace VitPro {

	partial struct Mat4 {

		public static Mat4 operator +(Mat4 matrix) {
			return matrix;
		}
		public static Mat4 operator -(Mat4 matrix) {
			var result = new Mat4();
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					result[i, j] = -matrix[i, j];
			return result;
		}

		public static Mat4 operator +(Mat4 a, Mat4 b) {
			return new Mat4(a.mat + b.mat);
		}
		public static Mat4 operator -(Mat4 a, Mat4 b) {
			return new Mat4(a.mat - b.mat);
		}

		public static Mat4 operator *(Mat4 matrix, double k) {
			return new Mat4(matrix.mat * (float)k);
		}
		public static Mat4 operator *(double k, Mat4 matrix) {
			return new Mat4(matrix.mat * (float)k);
		}

		public static Mat4 operator /(Mat4 matrix, double k) {
			return new Mat4(matrix.mat * (float)(1 / k));
		}

		public static Mat4 operator *(Mat4 a, Mat4 b) {
			return new Mat4(a.mat * b.mat);
		}

	}
	
}
