using System;

namespace VitPro.Engine.UI {

	partial class Element {

		void InitPositioning() {
			Margin = 0.1;
			Padding = 0.2;
            Size = new Vec2(1, 1);
            Origin = new Vec2(0.5, 0.5);
		}

		/// <summary>
		/// Gets or sets the margin.
		/// </summary>
		/// <value>The margin.</value>
		public double Margin { get; set; }

		/// <summary>
		/// Gets or sets the padding.
		/// </summary>
		/// <value>The padding.</value>
		public double Padding { get; set; }
		
		/// <summary>
		/// Gets or sets the size of the element.
		/// </summary>
		/// <value>The size.</value>
		public Vec2 Size { get; set; }

		/// <summary>
		/// Gets or sets the position of the element.
		/// </summary>
		/// <value>The position.</value>
		public Vec2 Position { get; set; }

		/// <summary>
		/// Gets or sets the position of bottom left corner.
		/// </summary>
		/// <value>Position.</value>
		public Vec2 BottomLeft {
			get { return Position - Vec2.CompMult(Size, Origin); }
			set { Position = value + Vec2.CompMult(Size, Origin); }
		}

		/// <summary>
		/// Gets or sets the position of top right corner.
		/// </summary>
		/// <value>Position.</value>
		public Vec2 TopRight {
			get { return BottomLeft + Size; }
			set { BottomLeft = value - Size; }
		}

		/// <summary>
		/// Gets or sets the position of top left corner.
		/// </summary>
		/// <value>Position.</value>
		public Vec2 TopLeft {
			get { return BottomLeft + new Vec2(0, Size.Y); }
			set { BottomLeft = value - new Vec2(0, Size.Y); }
		}

		/// <summary>
		/// Gets or sets the position of bottom right corner.
		/// </summary>
		/// <value>Position.</value>
		public Vec2 BottomRight {
			get { return BottomLeft + new Vec2(Size.X, 0); }
			set { BottomLeft = value - new Vec2(Size.X, 0); }
		}

		/// <summary>
		/// Gets or sets the position of right center.
		/// </summary>
		/// <value>Position.</value>
		public Vec2 MidRight {
			get { return BottomLeft + new Vec2(Size.X, Size.Y / 2); }
			set { BottomLeft = value - new Vec2(Size.X, Size.Y / 2); }
		}

		/// <summary>
		/// Gets or sets the position of left center.
		/// </summary>
		/// <value>Position.</value>
		public Vec2 MidLeft {
			get { return BottomLeft + new Vec2(0, Size.Y / 2); }
			set { BottomLeft = value - new Vec2(0, Size.Y / 2); }
		}

		/// <summary>
		/// Gets or sets the position of the center.
		/// </summary>
		/// <value>Position.</value>
		public Vec2 Center {
			get { return BottomLeft + Size / 2; }
			set { BottomLeft = value - Size / 2; }
		}

		/// <summary>
		/// Gets or sets the origin of the element.
		/// </summary>
		/// <value>The origin.</value>
		public Vec2 Origin { get; set; }

		/// <summary>
		/// Gets or sets the offset of the element.
		/// </summary>
		/// <value>The offset.</value>
		public Vec2 Offset { get; set; }

		/// <summary>
		/// Gets or sets the anchor.
		/// </summary>
		/// <value>The anchor.</value>
		public Vec2? Anchor { get; set; }

		void UpdatePosition() {
			if (Anchor.HasValue && Parent != null) {
				Position = Parent.BottomLeft + Vec2.CompMult(Parent.Size, Anchor.Value) + Offset;
			}
		}
		
	}

}
