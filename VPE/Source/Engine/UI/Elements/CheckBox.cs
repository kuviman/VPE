using System;

namespace VitPro.Engine.UI {

	/// <summary>
	/// UI check box.
	/// </summary>
	public class CheckBox : Button {

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.CheckBox"/> class.
		/// </summary>
		/// <param name="size">Size.</param>
		public CheckBox(double size) : base(null, null, size) {
			OnClick += () => {
				Checked = !Checked;
				if (OnChanged != null)
                    OnChanged.Invoke(Checked);
			};
			Size = new Vec2(size, size);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="VitPro.Engine.UI.CheckBox"/> is checked.
		/// </summary>
		/// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
		public bool Checked { get; set; }

		/// <summary>
		/// Occurs when check box is changed.
		/// </summary>
		public event Action<bool> OnChanged;

		/// <summary>
		/// Post render.
		/// </summary>
		protected override void PostRender() {
			base.PostRender();
			if (Checked) {
				RenderState.Push();
				RenderState.Color = BorderColor;
				Draw.Line(BottomLeft, TopRight, BorderWidth);
				Draw.Line(TopLeft, BottomRight, BorderWidth);
				RenderState.Pop();
			}
		}

	}

}
