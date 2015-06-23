using System;
using System.Collections.Generic;

namespace VitPro.Engine.Examples {

	class Physics : UI.State {
		
		class PhysicsState : State {

			class Body {
				public VitPro.Physics.Body body;
				Color color = Color.FromHSV(GRandom.NextDouble(), 1, 1);
				public Body(Vec2 pos) {
					int n = GRandom.Next(3, 6);
					Vec2[] vs = new Vec2[n];
					for (int i = 0; i < n; i++)
						vs[i] = Vec2.Rotate(Vec2.OrtX, i * 2 * Math.PI / n) * 50;
					body = new VitPro.Physics.Body(1, vs);
					body.Position = pos;
				}
				public void Render() {
					RenderState.Push();
					RenderState.Color = color;
					var vs = new List<Vec2>(body.Vertices).ToArray();
					Draw.Polygon(vs);
					RenderState.Color = Color.Black;
					for (int i = 0; i < vs.Length; i++) {
						int j = (i + 1) % vs.Length;
						Draw.Line(vs[i], vs[j], 5);
					}
					RenderState.Pop();
				}
			}

			List<Body> bodies = new List<Body>();

			Body current = null;
			Body last = null;

			Vec2? p1 = null;

			public override void MouseDown(MouseButton button, Vec2 position) {
				base.MouseDown(button, position);
				if (button == MouseButton.Left)
					current = new Body(position);
				else
					p1 = position;
			}

			public override void MouseUp(MouseButton button, Vec2 position) {
				base.MouseUp(button, position);
				if (button == MouseButton.Left) {
					current.body.Velocity = position - current.body.Position;
					bodies.Add(current);
					world.Add(current.body);
					last = current;
					current = null;
				} else {
					AddLine(p1.Value, position);
					p1 = null;
				}
			}

			public VitPro.Physics.World world = new VitPro.Physics.World();

			class Line {
				Vec2 p1, p2;
				public Line(Vec2 p1, Vec2 p2) {
					this.p1 = p1;
					this.p2 = p2;
				}
				public void Render() {
					RenderState.Push();
					RenderState.Color = Color.Black;
					Draw.Line(p1, p2, 10);
					RenderState.Pop();
				}
			}
			List<Line> lines = new List<Line>();

			void AddLine(Vec2 p1, Vec2 p2) {
				lines.Add(new Line(p1, p2));
				var body = new VitPro.Physics.Body(p1, p2);
				body.COF = Friction;
				body.COR = Restitution;
				world.Add(body);
			}

			public override void Update(double dt) {
				base.Update(dt);
				const int count = 1;
				if (last != null) {
					Vec2 v = Vec2.Zero;
					if (Key.W.Pressed())
						v.Y += 1;
					if (Key.A.Pressed())
						v.X -= 1;
					if (Key.S.Pressed())
						v.Y -= 1;
					if (Key.D.Pressed())
						v.X += 1;
					last.body.Velocity += v * 1000 * dt;
					double w = 0;
					if (Key.Left.Pressed())
						w += 1;
					if (Key.Right.Pressed())
						w -= 1;
					last.body.AngularVelocity += w * 10 * dt;
				}
				foreach (var body in world.Bodies) {
					body.COF = Friction;
					body.COR = Restitution;
				}
				for (int i = 0; i < count; i++) {
					var rdt = dt / count;
					foreach (var b in bodies)
						b.body.Velocity += new Vec2(0, -1000) * rdt;
					world.Update(rdt);
				}
			}

			double w = 1, h = 1;

			Vec2 mousePosition;

			public override void MouseMove(Vec2 position) {
				base.MouseMove(position);
				mousePosition = position;
			}

			public override void Render() {
				base.Render();
				w = RenderState.Width;
				h = RenderState.Height;
				Draw.Clear(Settings.BackgroundColor);
				RenderState.Push();
				RenderState.View2d(0, w, 0, h);
				foreach (var body in bodies)
					body.Render();
				foreach (var line in lines)
					line.Render();
				if (current != null) {
					current.Render();
					RenderState.Color = Color.Red;
					Draw.Line(current.body.Position, mousePosition, 3);
				}
				if (p1 != null) {
					RenderState.Color = Color.Blue;
					Draw.Line(p1.Value, mousePosition, 3);
				}
				RenderState.Pop();
			}

			public PhysicsState() {
				world.Add(new VitPro.Physics.Body(new Vec2(0, 0), new Vec2(1000, 0)));
			}

			public override void KeyDown(Key key) {
				base.KeyDown(key);
				if (key == Key.R) {
					world = new VitPro.Physics.World();
					bodies = new List<Body>();
					lines = new List<Line>();
				}
			}

			double _res = 0.7, _fr = 0.1;

			public double Restitution {
				get { return _res; }
				set {
					_res = value;
				}
			}

			public double Friction {
				get { return _fr; }
				set {
					_fr = value;
				}
			}

		}

		PhysicsState state;

		public Physics() {
			Zoom = Settings.ZoomUI;
			state = new PhysicsState();
			Background = state;
			var list = new UI.ElementList();
			list.Horizontal = true;
			list.Add(new UI.Label("Restitution:"));
			var resScale = new UI.Scale(100, 20);
			resScale.Value = state.Restitution;
			resScale.OnChanging += (double val) => state.Restitution = (val);
			list.Add(resScale);
			list.Add(new UI.Label("Friction:"));
			var frScale = new UI.Scale(100, 20);
			frScale.Value = state.Friction;
			frScale.OnChanging += (double val) => state.Friction = (val);
			list.Add(frScale);
			list.Anchor = list.Origin = new Vec2(0.5, 0);
			Frame.Add(list);
			Frame.Visit((elem) => {
				elem.TextColor = Color.Black;
			});
		}



	}

}
