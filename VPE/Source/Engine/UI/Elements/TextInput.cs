using System;

namespace VitPro.Engine.UI {

	/// <summary>
	/// Text input.
	/// </summary>
	public class TextInput : Element {

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.TextInput"/> class.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="textSize">Text size.</param>
		public TextInput(double width, double textSize = 20) {
			Width = width;
			TextSize = textSize;
			FocusedBorderColor = Color.Red;
			DefaultBorderColor = Color.Gray;
			BackgroundColor = new Color(0.9, 0.9, 0.9);
			TextColor = Color.Black;
			Value = "";
			Focusable = true;
		}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public double Width { get; set; }

		/// <summary>
		/// Gets or sets the value of the input.
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets the default color of the border.
		/// </summary>
		/// <value>The default color of the border.</value>
		public Color DefaultBorderColor { get; set; }

		/// <summary>
		/// Gets or sets the color of the border when input is focused.
		/// </summary>
		/// <value>The color of the border.</value>
		public Color FocusedBorderColor { get; set; }

		double blink = 0;

		/// <summary>
		/// Update this element.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public override void Update(double dt) {
			base.Update(dt);
			var padding = Padding * TextSize;
			Size = new Vec2(2 * padding + Width, 2 * padding + TextSize);
			blink += dt * 2;
			while (blink > 2)
				blink -= 2;
		}

		/// <summary>
		/// Render this element.
		/// </summary>
		public override void Render() {
			if (Focused)
				BorderColor = FocusedBorderColor;
			else
				BorderColor = DefaultBorderColor;
			base.Render();
			RenderState.Push();
			RenderState.Translate(MidLeft);
			var padding = Padding * TextSize;
			RenderState.Translate(padding + (Size.X - 2 * padding) * TextAlign, 0);
			RenderState.Color = TextColor;
			RenderState.Scale(TextSize);
			RenderState.Translate(RealFont.Measure(Value) * TextAlign, 0);
			RealFont.Render(Value + (blink < 1 ? "_" : ""), 0, 0.5);
			RenderState.Pop();
		}

		/// <summary>
		/// Occurs when value is changing.
		/// </summary>
		public event Action<string> OnChanging;

		/// <summary>
		/// Handles character input event.
		/// </summary>
		/// <param name="c">Character input.</param>
		public override void CharInput(char c) {
			base.CharInput(c);
			Value += c;
			if (OnChanging != null)
				OnChanging.Invoke(Value);	
		}

		/// <summary>
		/// Handles key down event.
		/// </summary>
		/// <param name="key">Key pressed.</param>
		public override void KeyDown(Key key) {
			base.KeyDown(key);
			if (key == Key.BackSpace)
				Backspace();
		}

		/// <summary>
		/// Handles key repeat event.
		/// </summary>
		/// <param name="key">Key repeated.</param>
		public override void KeyRepeat(Key key) {
			base.KeyRepeat(key);
			if (key == Key.BackSpace)
				Backspace();
		}

		void Backspace() {
			if (Value.Length != 0) {
				Value = Value.Substring(0, Value.Length - 1);
				if (OnChanging != null)
					OnChanging.Invoke(Value);
			}
		}

	}

}
