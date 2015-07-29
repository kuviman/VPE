using System;
using OpenTK.Graphics.OpenGL;

namespace VitPro.Engine {

	[Serializable]
	public class View {

		public Vec2 Position { get; set; }
		public double Rotation { get; set; }

		public double FOV { get; set; }

		public View(double fov) {
			FOV = fov;
			Position = Vec2.Zero;
			Rotation = 0;
		}

		public View Clone() {
			return MemberwiseClone() as View;
		}

		public void Apply() {
			float h = (float)(FOV / 2);
			float w = (float)(h * RenderState.Aspect);

			var rot = OpenTK.Matrix4d.CreateRotationZ((float)(-Rotation));
			var trans = OpenTK.Matrix4d.CreateTranslation((float)-Position.X, (float)-Position.Y, 0);
			var ortho = OpenTK.Matrix4d.CreateOrthographicOffCenter(-w, w, -h, h, -1, 1);
			RenderState.ProjectionMatrix = new Mat4(trans * rot * ortho);
		}

		public Vec2 FromWH(Vec2 pos, double width, double height) {
			pos = new Vec2(pos.X / width, pos.Y / height);
			double aspect = width / height;
			double h = FOV / 2;
			double w = h * aspect;
			double x = w * (2 * pos.X - 1);
			double y = h * (2 * pos.Y - 1);
			var result = Vec2.Rotate(new Vec2(x, y), Rotation);
			result += Position;
			return result;
		}

		public Vec2 OrtX { get { return Vec2.Rotate(Vec2.OrtX, Rotation); } }
		public Vec2 OrtY { get { return Vec2.Rotate(Vec2.OrtY, Rotation); } }

	}

}
