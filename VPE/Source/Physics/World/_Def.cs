using System;
using System.Collections.Generic;

namespace VitPro.Physics {

	/// <summary>
	/// World.
	/// </summary>
	public class World {

		List<Body> bodies = new List<Body>();

		/// <summary>
		/// Gets the bodies.
		/// </summary>
		/// <value>The bodies.</value>
		public IEnumerable<Body> Bodies {
			get {
				foreach (var body in bodies)
					yield return body;
			}
		}

		/// <summary>
		/// Add the specified body.
		/// </summary>
		/// <param name="body">Body.</param>
		public void Add(Body body) {
			bodies.Add(body);
		}

		/// <summary>
		/// Update the world.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public void Update(double dt) {
			foreach (var body in bodies) {
				body.Position += body.Velocity * dt;
				body.Rotation += body.AngularVelocity * dt;
			}

			foreach (var b1 in bodies) {
				if (b1.Static)
					continue;
				foreach (var b2 in bodies) {
					if (b1 == b2)
						continue;
					Collide(b1, b2);
				}
			}
		}

		void Collide(Body b1, Body b2) {
			if ((b1.Position - b2.Position).SqrLength > GMath.Sqr(b1.Radius + b2.Radius))
				return;
			Collision c12 = CollideDirect(b1, b2);
			Collision c21 = CollideDirect(b2, b1);
			Collision c = Collision.Min(c12, c21);
			if (c == null)
				return;
			if (c == c21)
				c.Normal = -c.Normal;

			if (c.Penetration < 0)
				return;

			if (b1.Static)
				b2.Position += c.Normal * c.Penetration;
			else if (b2.Static)
				b1.Position -= c.Normal * c.Penetration;
			else {
				b1.Position -= c.Normal * c.Penetration / 2;
				b2.Position += c.Normal * c.Penetration / 2;
			}

			Vec2 dv = b1.GetVelocity(c.Point) - b2.GetVelocity(c.Point);

			double z1 = -Vec2.Skew(c.Point - b1.Position, c.Normal) * b1.InvI;
			double z2 = -Vec2.Skew(c.Point - b2.Position, c.Normal) * b2.InvI;
			var j = Vec2.Dot(dv, c.Normal) / (b1.InvMass + b2.InvMass
				+ Vec2.Skew(c.Normal, (c.Point - b1.Position) * z1) 
				+ Vec2.Skew(c.Normal, (c.Point - b2.Position) * z2));

			if (j < 0)
				return;

			double E = Math.Sqrt(b1.COR * b2.COR);
			j = j * (1 + E);

			b1.ApplyImpulse(-j * c.Normal, c.Point);
			b2.ApplyImpulse(j * c.Normal, c.Point);

			Vec2 t = Vec2.Rotate90(c.Normal);
			if (Vec2.Dot(t, dv) < 0)
				t = -t;

			z1 = -Vec2.Skew(c.Point - b1.Position, t) * b1.InvI;
			z2 = -Vec2.Skew(c.Point - b2.Position, t) * b2.InvI;
			var maxJF = Vec2.Dot(dv, t) / (b1.InvMass + b2.InvMass
				+ Vec2.Skew(t, (c.Point - b1.Position) * z1) 
				+ Vec2.Skew(t, (c.Point - b2.Position) * z2));

			double EF = Math.Sqrt(b2.COF * b2.COF);
			var jf = Math.Min(j * EF, maxJF);

			b1.ApplyImpulse(-jf * t, c.Point);
			b2.ApplyImpulse(jf * t, c.Point);
		}

		Collision CollideDirect(Body b1, Body b2) {
			Collision result = null;
			var vs = new List<Vec2>(b1.Vertices);
			for (int i = 0; i < vs.Count; i++) {
				int j = (i + 1) % vs.Count;
				Vec2 p = vs[i];
				Vec2 n = Vec2.Rotate90(vs[i] - vs[j]).Unit;
				p += n * b1.Round;
				Collision cur = CollideWithLine(b2, p, n);
				result = Collision.Min(result, cur);
			}
			return result;
		}

		Collision CollideWithLine(Body body, Vec2 p, Vec2 n) {
			Collision result = null;
			foreach (var v in body.Vertices) {
				Collision cur = new Collision();
				cur.Penetration = Vec2.Dot(n, p - v);
				cur.Point = v;
				cur.Normal = n;
				result = Collision.Max(result, cur);
			}
			return result;
		}

		class Collision {
			public double Penetration;
			public Vec2 Normal;
			public Vec2 Point;

			public static Collision Min(Collision a, Collision b) {
				if (a == null)
					return b;
				if (b == null)
					return a;
				return a.Penetration < b.Penetration ? a : b;
			}

			public static Collision Max(Collision a, Collision b) {
				if (a == null)
					return b;
				if (b == null)
					return a;
				return a.Penetration > b.Penetration ? a : b;
			}
		}

	}

}
