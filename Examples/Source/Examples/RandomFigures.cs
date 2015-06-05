using System;
using System.Collections.Generic;

namespace VitPro.Engine.Examples {

	class RandomFigures : State {

		class Figure {
			public Vec2[] vertices;
			public Color color;
			public Vec2 pos;
			public double w, a;
		}

		List<Figure> figures = new List<Figure>();

		public override void MouseDown(MouseButton button, Vec2 position) {
			base.MouseDown(button, position);
			var figure = new Figure();
			int n = GRandom.Next(3, 9);
			figure.vertices = new Vec2[n];
			for (int i = 0; i < n; i++)
				figure.vertices[i] = Vec2.Rotate(Vec2.OrtX * 40, i * 2 * Math.PI / n);
			figure.color = Color.FromHSV(GRandom.NextDouble(), 1, 1, 0.5);
			figure.pos = Mouse.Position;
			figure.a = GRandom.NextDouble(0, 2 * Math.PI);
			figure.w = GRandom.NextDouble(-1, 1);
			figures.Add(figure);
		}

		public override void Update(double dt) {
			base.Update(dt);
			foreach (var figure in figures)
				figure.a += figure.w * dt;
		}

		public override void Render() {
			base.Render();
			Draw.Clear(0.8, 0.8, 1);
			RenderState.Push();
			RenderState.View2d(0, RenderState.Width, 0, RenderState.Height);
			foreach (var figure in figures) {
				RenderState.Push();
				RenderState.Color = figure.color;
				RenderState.Translate(figure.pos);
				RenderState.Rotate(figure.a);
				Draw.Polygon(figure.vertices);
				RenderState.Color = Color.Black;
				for (int i = 0; i < figure.vertices.Length; i++) {
					Draw.Line(figure.vertices[i], figure.vertices[(i + 1) % figure.vertices.Length], 6);
					Draw.Circle(figure.vertices[i], 3);
				}
				RenderState.Pop();
			}
			RenderState.Pop();
		}

	}

}
