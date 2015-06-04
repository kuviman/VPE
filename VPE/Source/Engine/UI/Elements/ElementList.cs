using System;

namespace VitPro.Engine.UI {

	/// <summary>
	/// UI element list.
	/// </summary>
	public class ElementList : Element {

        /// <summary>
        /// Initializes new UI element list.
        /// </summary>
        public ElementList() {
            Spacing = 10;
        }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="VitPro.Engine.UI.ElementList"/> is horizontal.
		/// </summary>
		/// <value><c>true</c> if horizontal; otherwise, <c>false</c>.</value>
		public bool Horizontal { get; set; }

		/// <summary>
		/// Gets or sets the spacing between elements.
		/// </summary>
		/// <value>The spacing.</value>
		public double Spacing { get; set; }

		/// <summary>
		/// Update this element.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public override void Update(double dt) {
			base.Update(dt);
			double w = 0, h = -Spacing;
			foreach (var child in Children) {
				double curW = child.Size.X, curH = child.Size.Y;
				if (Horizontal)
					GUtil.Swap(ref curW, ref curH);
				w = Math.Max(w, curW);
				h += curH + Spacing;
			}
			if (Horizontal)
				GUtil.Swap(ref w, ref h);
			Size = new Vec2(w, h);

			double pos = Horizontal ? BottomLeft.X : TopRight.Y;
			foreach (var child in Children) {
				double nextPos = pos + (Horizontal ? child.Size.X : -child.Size.Y);
				double mid = (pos + nextPos) / 2;
				if (Horizontal)
					child.Position = new Vec2(mid, Center.Y);
				else
					child.Position = new Vec2(Center.X, mid);
				pos = nextPos + (Horizontal ? Spacing : -Spacing);
			}
		}
		
	}

}
