using System;
using System.Collections.Generic;

namespace VitPro.Engine.Examples {

	class Selector : UI.State {

		class SelectElement<T> : ExampleSelectElement<T> where T : State, new() {
			public SelectElement() {
				button = new UI.Button(typeof(T).Name, null, 20);
				button.Anchor = button.Origin = Vec2.Zero;
                button.FixedWidth = 500;
				Add(button);

				Origin = new Vec2(0, 0.5);
			}

			UI.Button button;

			public override void Update(double dt) {
				base.Update(dt);
				Size = button.Size;
			}
		}

		static List<UI.Element> elements = new List<VitPro.Engine.UI.Element>();

		public static void Register<T>() where T : State, new() {
			elements.Add(new SelectElement<T>());
		}

		public Selector() {
			foreach (var elem in elements)
				Frame.Add(elem);
		}

		public override void Update(double dt) {
			base.Update(dt);
			Zoom = Settings.ZoomUI;
			double margin = 10;
			double curW = -margin;
			double curY = 0;
			double rowSize = 40;
			var cur = new List<UI.Element>();
			Frame.Visit(elem => {
				if (elem is ExampleSelectElement) {
					if (curW + elem.Size.X + margin > Frame.Size.X - 100) {
						foreach (var e in cur) {
							e.Position += new Vec2(Frame.Size.X / 2 - curW / 2, 0);
						}
						cur = new List<UI.Element>();
						curW = -margin;
						curY -= rowSize;
					}
					elem.Position = new Vec2(curW + margin, curY);
					curW += elem.Size.X + margin;
					cur.Add(elem);
				}
			});
			foreach (var e in cur) {
				e.Position += new Vec2(Frame.Size.X / 2 - curW / 2, 0);
			}
			double midY = (curY - rowSize) / 2;
			Frame.Visit(elem => {
				elem.Position += new Vec2(0, Frame.Size.Y / 2 - midY);
			});
		}

		public override void Render() {
			Draw.Clear(Settings.BackgroundColor);
			base.Render();
		}

	}

}
