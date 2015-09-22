using System;
using System.Collections.Generic;

namespace VitPro.Physics {
	
	public partial class Body {

		Vec2[] vertices;

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public Vec2 Position { get; set; }

		/// <summary>
		/// Gets or sets the velocity.
		/// </summary>
		/// <value>The velocity.</value>
		public Vec2 Velocity { get; set; }

		/// <summary>
		/// Gets or sets the rotation.
		/// </summary>
		/// <value>The rotation.</value>
		public double Rotation { get; set; }

		/// <summary>
		/// Gets or sets the angular velocity.
		/// </summary>
		/// <value>The angular velocity.</value>
		public double AngularVelocity { get; set; }

		/// <summary>
		/// Gets the inversed mass.
		/// </summary>
		/// <value>The inversed mass.</value>
		public double InvMass { get; private set; }

		double? _mass;

		/// <summary>
		/// Gets or sets the mass.
		/// </summary>
		/// <value>The mass.</value>
		public double? Mass {
			get { return _mass; }
			set {
				_mass = value;
				if (value.HasValue)
					InvMass = 1 / value.Value;
				else
					InvMass = 0;
			}
		}

		/// <summary>
		/// Gets the inversed moment of inertia.
		/// </summary>
		/// <value>The inversed moment of inertia.</value>
		public double InvI { get; private set; }

		double? _I;

		/// <summary>
		/// Gets or sets the moment of inertia.
		/// </summary>
		/// <value>The moment of inertia.</value>
		public double? I {
			get { return _I; }
			set {
				_I = value;
				if (value.HasValue)
					InvI = 1 / value.Value;
				else
					InvI = 0;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="VitPro.Physics.Body"/> is static.
		/// </summary>
		/// <value><c>true</c> if static; otherwise, <c>false</c>.</value>
		public bool Static {
			get { return Mass == null; }
		}

		/// <summary>
		/// Gets or sets the round.
		/// </summary>
		/// <value>The round.</value>
		public double Round { get; set; }

		/// <summary>
		/// Gets or sets the coefficient of restitution.
		/// </summary>
		/// <value>The coefficient of restitution.</value>
		public double COR { get; set; }

		/// <summary>
		/// Gets or sets the coefficient of friction.
		/// </summary>
		/// <value>The coefficient of friction.</value>
		public double COF { get; set; }

		Body() {
			Round = 0;
			COR = 0.7;
			COF = 0.1;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Physics.Body"/> class.
		/// </summary>
		/// <param name="density">Density.</param>
		/// <param name="vertices">Vertices.</param>
		public Body(double density, params Vec2[] vertices) : this() {
			double m = 0;
			double I = 0;
			Vec2 center = Vec2.Zero;
			for (int i = 0; i < vertices.Length; i++) {
				int j = (i + 1) % vertices.Length;
				m += Vec2.Skew(vertices[i], vertices[j]);
				I += Vec2.Skew(vertices[i], vertices[j])
					* (vertices[i].SqrLength + vertices[j].SqrLength + Vec2.Dot(vertices[i], vertices[j]));
				center += Vec2.Skew(vertices[i], vertices[j]) * (vertices[i] + vertices[j]);
			}
			this.Mass = density * Math.Abs(m);
			this.I = density * Math.Abs(I) / 6;
			center = center / (3 * m);
			this.vertices = new Vec2[vertices.Length];
			for (int i = 0; i < vertices.Length; i++)
				this.vertices[i] = vertices[i] - center;
			if (m < 0)
				Array.Reverse(this.vertices);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Physics.Body"/> class.
		/// </summary>
		/// <param name="vertices">Vertices.</param>
		public Body(params Vec2[] vertices) : this() {
			this.vertices = new Vec2[vertices.Length];
			Mass = null;
			I = null;
			for (int i = 0; i < vertices.Length; i++)
				this.vertices[i] = vertices[i];
		}

		/// <summary>
		/// Gets the vertices.
		/// </summary>
		/// <value>The vertices.</value>
		public IEnumerable<Vec2> Vertices {
			get {
				foreach (var v in vertices)
					yield return Position + Vec2.Rotate(v, Rotation);
			}
		}

		/// <summary>
		/// Gets the radius.
		/// </summary>
		/// <value>The radius.</value>
		public double Radius {
			get {
				double r = 0;
				foreach (var v in vertices)
					r = Math.Max(r, v.Length);
				return r;
			}
		}

	}

}
