using System;

namespace VitPro.Engine.Examples {

	class Settings : UI.State {

		public static Color BackgroundColor = Color.Sky;

		public static double ZoomUI = 2;

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

			ColorSelector colorSel = new ColorSelector();
			colorSel.Color = BackgroundColor;
			colorSel.Size = new Vec2(100, 100);
			colorSel.OnChange += (color) => {
				BackgroundColor = color;
			};
			var colorList = new UI.ElementList();
			colorList.Add(new UI.Label("Background color", 20));
			colorList.Horizontal = true;
			colorList.Add(colorSel);
			list.Add(colorList);

			list.Anchor = list.Origin = new Vec2(0.5, 0.5);
			Frame.Add(list);

			Frame.Visit(elem => {
				if (elem is UI.Label)
					elem.TextColor = Color.Black;
			});
		}

		public override void Update(double dt) {
			base.Update(dt);
//			Zoom = ZoomUI;
		}

		public override void Render() {
			Draw.Clear(BackgroundColor);
			base.Render();
		}

		static Settings instance = new Settings();

		class SelectElement : RoundSelectElement {
			public SelectElement() : base("S") {}
			public override State GetState() {
				return instance;
			}
		}

		public static ExampleSelectElement GetSelectElement() {
			return new SelectElement();
		}
	}

}
