using System;

namespace VitPro.Engine {

	/// <summary>
	/// Interface representing an updateable object.
	/// </summary>
	public interface IUpdateable {

		/// <summary>
		/// Update the object.
		/// </summary>
		/// <param name="dt">Time elapsed since last update.</param>
		void Update(double dt);

	}

}
