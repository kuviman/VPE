using System;
using System.Collections.Generic;

namespace VitPro.Engine.Examples {

	class Physics : State {

		class Body {
			public Vec2[] vs;
			public Vec2 p, v;
			public double a, w;
			public double m, I;

			public Color color;

			public Body(Vec2 pos) {
				p = pos;
				int n = GRandom.Next(3, 8);
				vs = new Vec2[n];
				for (int i = 0; i < n; i++)
					vs[i] = Vec2.Rotate(Vec2.OrtX * 100, i * 2 * Math.PI / n);
				color = Color.FromHSV(GRandom.NextDouble(), 1, 1, 0.5);
				a = GRandom.NextDouble(0, 2 * Math.PI);
				v = Vec2.Zero;
				w = 0;
				m = 1;
				I = 0;
				double div = 0;
				for (int i = 0; i < n; i++) {
					int j = (i + 1) % n;
					I += Vec2.Skew(vs[i], vs[j]) * (vs[i].SqrLength + vs[j].SqrLength + Vec2.Dot(vs[i], vs[j]));
					div += Vec2.Skew(vs[i], vs[j]);
				}
				I = m / 6 * I / div;
			}

			public void Apply(Vec2 impulse, Vec2 point) {
				v += impulse / m;
				w += Vec2.Skew(point - p, impulse) / I;
			}

			public Vec2 getV(Vec2 point) {
				return v + Vec2.Rotate90(point - p) * w;
			}

			public void Update(double dt) {
				p += v * dt;
				a += w * dt;
			}

			public void Render() {
				RenderState.Push();
				RenderState.Translate(p);
				RenderState.Rotate(a);
				RenderState.Color = color;
				Draw.Polygon(vs);
				RenderState.Color = Color.Black;
				for (int i = 0; i < vs.Length; ++i) {
					int j = (i + 1) % vs.Length;
					Draw.Line(vs[i], vs[j], 4);
					Draw.Circle(vs[i], 2);
				}
				RenderState.Pop();
			}

			public IEnumerable<Vec2> points {
				get {
					foreach (var p in vs) {
						yield return this.p + Vec2.Rotate(p, a);
					}
				}
			}
		}

		static Tuple<Double,Vec2> Collide(Body body, Vec2 p, Vec2 n) {
			double pen = -1e9;
			Vec2 r = Vec2.Zero;
			foreach (var point in body.points) {
				var cur = Vec2.Dot(p - point, n);
				if (cur > pen) {
					pen = cur;
					r = point;
				}
			}
			return Tuple.Create(pen, r);
		}

		static Tuple<Double,Vec2,Vec2> Collide(Body b1, Body b2) {
			double pen = 1e9;
            Vec2 p = Vec2.Zero;
			Vec2 n = Vec2.Zero;
			var v1 = new List<Vec2>(b1.points).ToArray();
			var v2 = new List<Vec2>(b2.points).ToArray();
			for (int i = 0; i < v1.Length; i++) {
				var curN = Vec2.Rotate90(v1[i] - v1[(i + 1) % v1.Length]).Unit;
				Tuple<Double, Vec2> cur = Collide(b2, v1[i], curN);
				if (cur.Item1 < pen) {
					pen = cur.Item1;
					p = cur.Item2;
					n = -curN;
				}
			}
			for (int i = 0; i < v2.Length; i++) {
				var curN = Vec2.Rotate90(v2[i] - v2[(i + 1) % v2.Length]).Unit;
				Tuple<Double, Vec2> cur = Collide(b1, v2[i], curN);
				if (cur.Item1 < pen) {
					pen = cur.Item1;
					p = cur.Item2;
					n = curN;
				}
			}
			return Tuple.Create(pen, p, n);
		}

		static double CalcJ(double im1, double im2, Vec2 r1, Vec2 r2, double iI1, double iI2, Vec2 v1, Vec2 v2, Vec2 n) {
			const double E = 0.5;
			double z1 = -Vec2.Skew(r1, n) * iI1;
			double z2 = -Vec2.Skew(r2, n) * iI1;
			var j = (1 + E) * Vec2.Dot(v2 - v1, n) / (im1 + im2 + Vec2.Skew(n, r1 * z1) + Vec2.Skew(n, r2 * z2));
			return Math.Max(j, 0);
		}

		List<Body> bodies = new List<Body>();

		Body current = null;

		public override void MouseDown(MouseButton button, Vec2 position) {
			base.MouseDown(button, position);
			current = new Body(position);
		}

		public override void MouseUp(MouseButton button, Vec2 position) {
			base.MouseUp(button, position);
			current.v = position - current.p;
			bodies.Add(current);
			current = null;
		}

		public override void Update(double dt) {
			base.Update(dt);
			const int count = 1;
			for (int i = 0; i < count; i++) {
				Tick(dt / count);
			}
		}

		void Tick(double dt) {
			foreach (var body in bodies) {
				body.v += new Vec2(0, -100) * dt;
				body.Update(dt);
				checkWall(body, new Vec2(0, 0), new Vec2(1, 0));
				checkWall(body, new Vec2(0, 0), new Vec2(0, 1));
				checkWall(body, new Vec2(w, 0), new Vec2(-1, 0));
				checkWall(body, new Vec2(0, h), new Vec2(0, -1));
				foreach (var b2 in bodies) {
					if (body != b2)
						check(body, b2);
				}
			}
		}

		static void check(Body b1, Body b2) {
			Tuple<double, Vec2, Vec2> r = Collide(b1, b2);
			if (r.Item1 < 0)
				return;

			b1.p += r.Item1 * r.Item3 / 2;
			b2.p -= r.Item1 * r.Item3 / 2;

			var j = CalcJ(1 / b1.m, 1 / b2.m, r.Item2 - b1.p, r.Item2 - b2.p, 1 / b1.I, 1 / b2.I, b1.getV(r.Item2), b2.getV(r.Item2), r.Item3);
			b1.Apply(j * r.Item3, r.Item2);
			b2.Apply(-j * r.Item3, r.Item2);
		}

		static void checkWall(Body b, Vec2 p, Vec2 n) {
			Tuple<double, Vec2> pen = Collide(b, p, n);
			if (pen.Item1 < 0)
				return;
			
			b.p += pen.Item1 * n;

			var o = pen.Item2;
			var J = CalcJ(1 / b.m, 0, o - b.p, Vec2.Zero, 1 / b.I, 0, b.getV(o), Vec2.Zero, n);
			b.Apply(J * n, o);
		}

		double w = 1, h = 1;

		public override void Render() {
			base.Render();
			w = RenderState.Width;
			h = RenderState.Height;
			Draw.Clear(Settings.BackgroundColor);
			RenderState.Push();
			RenderState.View2d(0, w, 0, h);
			foreach (var body in bodies)
				body.Render();
			if (current != null) {
				current.Render();
				RenderState.Color = Color.Red;
				Draw.Line(current.p, Mouse.Position, 3);
			}
			RenderState.Pop();
		}

	}

}
