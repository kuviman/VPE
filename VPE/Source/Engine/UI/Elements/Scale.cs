using System;

namespace VitPro.Engine.UI {

	/// <summary>
	/// UI scale element.
	/// </summary>
	public class Scale : Element {

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.Scale"/> class.
		/// </summary>
		/// <param name="length">Length.</param>
		/// <param name="size">Size.</param>
		public Scale(double length, double size) {
			Size = new Vec2(length, size);
			BorderColor = Color.Gray;
			BackgroundColor = new Color(0.9, 0.9, 0.9);
		}

		double _position = 0.5;

		/// <summary>
		/// Gets or sets the value of the scale.
		/// </summary>
		/// <value>The position (0 - 1).</value>
		public double Value {
			get { return _position; }
			set { _position = Math.Min(1, Math.Max(0, value)); }
		}

		/// <summary>
		/// Render this element.
		/// </summary>
		public override void Render() {
			base.Render();
			RenderState.Push();
			RenderState.Color = BorderColor;
			Draw.Line(MidLeft, MidRight, BorderWidth / 2);
			RenderState.Translate(BottomLeft + new Vec2(Value * Size.X, 0));
			RenderState.Translate(0, Size.Y / 2);
			if (Pressed)
				RenderState.Scale(0.8);
			RenderState.Origin(0, Size.Y / 2);
			RenderState.Color = Hovered ? new Color(0.8, 0.8, 1) : new Color(0.8, 0.8, 0.8);
			double wid = 5;
			Draw.Rect(-wid, 0, wid, Size.Y);
			RenderState.Color = new Color(0.5, 0.5, 0.5);
			Draw.Frame(-wid, 0, wid, Size.Y, BorderWidth);
			RenderState.Pop();
		}

		double lastPos = 0.5;

		/// <summary>
		/// Handles mouse move events.
		/// </summary>
		/// <param name="position">Mouse position.</param>
		public override void MouseMove(Vec2 position) {
			base.MouseMove(position);
			lastPos = (position.X - BottomLeft.X) / Size.X;
			if (Pressed) {
				Value = lastPos;
				if (OnChanging != null)
					OnChanging.Invoke(Value);
			}
		}

		public override void Press() {
			base.Press();
			if (OnChanging != null) {
				Value = lastPos;
				if (OnChanging != null)
					OnChanging.Invoke(Value);
			}
		}

		/// <summary>
		/// Handles release events.
		/// </summary>
		public override void Release() {
			base.Release();
			if (OnChanged != null)
				OnChanged.Invoke(Value);
		}

		/// <summary>
		/// Triggered when value is changed.
		/// </summary>
		/// <value>Handler.</value>
		public event Action<double> OnChanged;

		/// <summary>
		/// Triggered when scale is being dragged.
		/// </summary>
		/// <value>Handler.</value>
		public event Action<double> OnChanging;

	}

}
