using System;

namespace VitPro.Engine.Examples {

	class Selector : UI.State {

		public override void Render() {
			Draw.Clear(Settings.BackgroundColor);
			base.Render();
		}

	}

}
