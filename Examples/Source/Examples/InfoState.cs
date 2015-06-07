using System;

namespace VitPro.Engine.Examples {

	class InfoState : UI.State {

		public InfoState() {
			var info = Resource.String("Info").Split(new string[]{"====="}, StringSplitOptions.None);
			var title = new UI.Label(info[0].Trim(), 32);
			var subtitle = new UI.Label(info[1].Trim(), 14);
			var text = new UI.Label(info[2].Trim(), 20);

			var list = new UI.ElementList();
			list.Add(title);
			list.Add(subtitle);
			list.Add(new UI.Frame(0, 20));
			list.Add(text);
			list.Anchor = new Vec2(0.5, 0.5);
			Frame.Add(list);

			Frame.Visit((elem) => {
				elem.TextColor = Color.Black;
			});
		}

		public override void Update(double dt) {
			base.Update(dt);
			Zoom = Settings.ZoomUI;
		}

		public override void Render() {
			Draw.Clear(Settings.BackgroundColor);
			base.Render();
		}

		static InfoState instance = new InfoState();

		public class SelectElement : RoundSelectElement {
			public SelectElement() : base("?") {}
			public override State GetState() {
				return instance;
			}
		}

		public static UI.Element GetSelectElement() {
			return new SelectElement();
		}
		
	}

}
