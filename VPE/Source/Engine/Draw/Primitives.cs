using System;

namespace VitPro.Engine {
	
	partial class Draw {

		/// <summary>
		/// Draw a quad.
		/// </summary>
		public static void Quad() {
			Shader.Std.Color.RenderQuad();
		}

		/// <summary>
		/// Draw a rectangle.
		/// </summary>
		/// <param name="x1">X coordinate of the first corner.</param>
		/// <param name="y1">Y coordinate of the first corner.</param>
		/// <param name="x2">X coordinate of the second corner.</param>
		/// <param name="y2">Y coordinate of the second corner.</param>
		public static void Rect(double x1, double y1, double x2, double y2) {
			RenderState.Push();
			RenderState.Translate(x1, y1);
			RenderState.Scale(x2 - x1, y2 - y1);
			Quad();
			RenderState.Pop();
		}

		/// <summary>
		/// Draw a rectangle.
		/// </summary>
		/// <param name="p1">First corner.</param>
		/// <param name="p2">Second corner.</param>
		public static void Rect(Vec2 p1, Vec2 p2) {
			Rect(p1.X, p1.Y, p2.X, p2.Y);
		}

		/// <summary>
		/// Draw a rectangle.
		/// </summary>
		/// <param name="p1">First corner.</param>
		/// <param name="p2">Second corner.</param>
		/// <param name="color">Color.</param>
		public static void Rect(Vec2 p1, Vec2 p2, Color color) {
			RenderState.Push();
			RenderState.Color = color;
			Rect(p1.X, p1.Y, p2.X, p2.Y);
			RenderState.Pop();
		}

		/// <summary>
		/// Draw a line.
		/// </summary>
		/// <param name="p1">First point.</param>
		/// <param name="p2">Second point.</param>
		/// <param name="width">Width of the line.</param>
		public static void Line(Vec2 p1, Vec2 p2, double width) {
			Vec2 v = p2 - p1;
			RenderState.Push();
			RenderState.SetOrts(v, Vec2.Rotate90(v.Unit) * width, p1);
			RenderState.Origin(0, 0.5);
			Quad();
			RenderState.Pop();
		}

		/// <summary>
		/// Draw a line.
		/// </summary>
		/// <param name="x1">X coordinate of the first point.</param>
		/// <param name="y1">Y coordinate of the first point.</param>
		/// <param name="x2">X coordinate of the second point.</param>
		/// <param name="y2">Y coordinate of the second point.</param>
		/// <param name="width">Width of the line.</param>
		public static void Line(double x1, double y1, double x2, double y2, double width) {
			Line(new Vec2(x1, y1), new Vec2(x2, y2), width);
		}

		/// <summary>
		/// Draw a frame.
		/// </summary>
		/// <param name="x1">X coordinate of the first corner.</param>
		/// <param name="y1">Y coordinate of the first corner.</param>
		/// <param name="x2">X coordinate of the second corner.</param>
		/// <param name="y2">Y coordinate of the second corner.</param>
		/// <param name="width">Width.</param>
		public static void Frame(double x1, double y1, double x2, double y2, double width) {
			Line(x1, y1, x2, y1, width);
			Line(x2, y1, x2, y2, width);
			Line(x2, y2, x1, y2, width);
			Line(x1, y2, x1, y1, width);
		}

		/// <summary>
		/// Draw a frame.
		/// </summary>
		/// <param name="p1">First corner.</param>
		/// <param name="p2">Second corner.</param>
		/// <param name="width">Width.</param>
		public static void Frame(Vec2 p1, Vec2 p2, double width) {
			Frame(p1.X, p1.Y, p2.X, p2.Y, width);
		}

		/// <summary>
		/// Draw a circle.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="radius">Radius.</param>
		public static void Circle(double x, double y, double radius) {
			RenderState.Push();
			RenderState.Translate(x, y);
			RenderState.Scale(radius * 2);
			RenderState.Origin(0.5, 0.5);
			Shader.Std.Circle.RenderQuad();
			RenderState.Pop();
		}

		/// <summary>
		/// Draw a circle.
		/// </summary>
		/// <param name="position">Position.</param>
		/// <param name="radius">Radius.</param>
		public static void Circle(Vec2 position, double radius) {
			Circle(position.X, position.Y, radius);
		}

		/// <summary>
		/// Render a box using a texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		public static void Box(Texture texture) {
			RenderState.Push();
			RenderState.Translate(0.5, 0.5, 0.5);

			for (int i = 0; i < 4; i++) {
				RenderState.RotateY(Math.PI / 2);
				RenderState.Push();
				RenderState.Origin(0.5, 0.5, 0.5);
				texture.Render();
				RenderState.Pop();
			}

			RenderState.RotateX(Math.PI / 2);
			RenderState.Push();
			RenderState.Origin(0.5, 0.5, 0.5);
			texture.Render();
			RenderState.Pop();

			RenderState.RotateX(Math.PI);
			RenderState.Push();
			RenderState.Origin(0.5, 0.5, 0.5);
			texture.Render();
			RenderState.Pop();

			RenderState.Pop();
		}

		/// <summary>
		/// Render a textured rectangle.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="x1">X coordinate of the first corner.</param>
		/// <param name="y1">Y coordinate of the first corner.</param>
		/// <param name="x2">X coordinate of the second corner.</param>
		/// <param name="y2">Y coordinate of the second corner.</param>
		public static void Texture(Texture texture, double x1, double y1, double x2, double y2) {
			RenderState.Push();
			RenderState.Translate(x1, y1);
			RenderState.Scale(x2 - x1, y2 - y1);
			texture.Render();
			RenderState.Pop();
		}

		/// <summary>
		/// Render a textured rectangle.
		/// </summary>
		/// <param name="texture">Texture.</param>
		/// <param name="p1">First corner.</param>
		/// <param name="p2">Second corner.</param>
		public static void Texture(Texture texture, Vec2 p1, Vec2 p2) {
			Texture(texture, p1.X, p1.Y, p2.X, p2.Y);
		}

		/// <summary>
		/// Render a polygon.
		/// </summary>
		/// <param name="points">Points.</param>
		public static void Polygon(params Vec2[] points) {
			Shader.Std.Color.RenderPolygon(points);
		}

	}

}
