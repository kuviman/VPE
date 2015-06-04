using System;

namespace VitPro.Engine.Examples {

	class Settings : UI.State {

		public static double ZoomUI = 1;

		public Settings() {
			Zoom = ZoomUI;

			var list = new UI.ElementList();

			var fullscreenLabel = new UI.Label("Fullscreen", 20);
			fullscreenLabel.TextColor = Color.Black;
			var fullscreenCheckBox = new UI.CheckBox(20);
			fullscreenCheckBox.Checked = App.Fullscreen;
			fullscreenCheckBox.OnChanged += (value) => App.Fullscreen = value;
			var fullscreenList = new UI.ElementList();
			fullscreenList.Horizontal = true;
			fullscreenList.Add(fullscreenLabel);
			fullscreenList.Add(fullscreenCheckBox);
			list.Add(fullscreenList);

			var zoomLabel = new UI.Label("UI zoom", 20);
			zoomLabel.TextColor = Color.Black;
			var zoomScale = new UI.Scale(100, 20);
			zoomScale.Value = (ZoomUI - 0.5) / 3;
			zoomScale.OnChanging += (value) => ZoomUI = 0.5 + value * 3;
			zoomScale.OnChanged += (value) => Zoom = ZoomUI = 0.5 + value * 3;
			var zoomList = new UI.ElementList();
			zoomList.Horizontal = true;
			zoomList.Add(zoomLabel);
			zoomList.Add(zoomScale);
			list.Add(zoomList);

			list.Anchor = list.Origin = new Vec2(0.5, 0.5);
			Frame.Add(list);
		}

		public override void Update(double dt) {
			base.Update(dt);
//			Zoom = ZoomUI;
		}

		public override void Render() {
			Draw.Clear(0.8, 0.8, 1);
			base.Render();
		}
	}

}
