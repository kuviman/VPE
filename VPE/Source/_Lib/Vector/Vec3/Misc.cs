using System;

namespace VitPro {

    partial struct Vec3 {

        /// <summary>
        /// Clamp the length of a vector.
        /// </summary>
        /// <param name="a">Vector to clamp.</param>
        /// <param name="maxlen">Maximal length.</param>
        /// <returns>Clamped vector.</returns>
        public static Vec3 Clamp(Vec3 a, double maxlen) {
            if (a.Length <= maxlen)
                return a;
            else
                return a.Unit * maxlen;
        }

		public Vec2 XY {
			get { return new Vec2(X, Y); }
		}

    }

}