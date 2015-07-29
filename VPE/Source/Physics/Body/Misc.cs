using System;

namespace VitPro.Physics {

	partial class Body {

		/// <summary>
		/// Applies the impulse.
		/// </summary>
		/// <param name="impulse">Impulse.</param>
		/// <param name="point">Point.</param>
		public void ApplyImpulse(Vec2 impulse, Vec2 point) {
			Velocity += impulse * InvMass;
			AngularVelocity += Vec2.Skew(point - Position, impulse) * InvI;
		}

		/// <summary>
		/// Gets the velocity of a point on the body.
		/// </summary>
		/// <returns>The velocity.</returns>
		/// <param name="point">Point.</param>
		public Vec2 GetVelocity(Vec2 point) {
			return Velocity + Vec2.Rotate90(point - Position) * AngularVelocity;
		}

	}

}
