using System;

namespace VitPro.Engine.Examples {

	class ColorSelector : UI.Element {

		const double eps = 1 / 256.0;

		class MainSelect : UI.Element {
			static Shader shader = new Shader(Resource.String("ColorSelect.glsl"));
			ColorSelector selector;

			public MainSelect(ColorSelector sel) {
				selector = sel;
				BorderColor = Color.Black;
			}

			public override void MouseDown(MouseButton button, Vec2 position) {
				base.MouseDown(button, position);
				if (button == MouseButton.Left)
					selectAt(position);
			}

			public override void MouseMove(Vec2 position) {
				base.MouseMove(position);
				if (Pressed)
					selectAt(position);
			}

			void selectAt(Vec2 pos) {
				pos = Vec2.CompDiv(pos - BottomLeft, Size);
				selector.Color = Color.FromHSV(GMath.Clamp(pos.X, 0 + eps, 1 - eps),
					GMath.Clamp(pos.Y, 0 + eps, 1 - eps), selector.Color.V);
			}

			public override void Render() {
				RenderState.Push();
				RenderState.Translate(BottomLeft);
				RenderState.Scale(Size);
				shader.RenderQuad();
				RenderState.Pop();

				RenderState.Push();
				RenderState.Translate(BottomLeft);
				RenderState.Scale(Size);
				RenderState.Translate(selector.Color.H, selector.Color.S);
				RenderState.Scale(1 / Size.X, 1 / Size.Y);
				RenderState.Color = Color.Black;
				Draw.Circle(0, 0, 3);
				RenderState.Color = Color.White;
				Draw.Circle(0, 0, 2);
				RenderState.Pop();

				base.Render();
			}
		}

		class ValueSelect : UI.Element {
			static Shader shader = new Shader(Resource.String("ColorSelectValue.glsl"));
			ColorSelector selector;

			public ValueSelect(ColorSelector sel) {
				selector = sel;
				BorderColor = Color.Black;
			}

			public override void MouseDown(MouseButton button, Vec2 position) {
				base.MouseDown(button, position);
				if (button == MouseButton.Left)
					selectAt(position);
			}

			public override void MouseMove(Vec2 position) {
				base.MouseMove(position);
				if (Pressed)
					selectAt(position);
			}

			void selectAt(Vec2 pos) {
				pos = Vec2.CompDiv(pos - BottomLeft, Size);
				selector.Color = Color.FromHSV(selector.Color.H, selector.Color.S, 
					GMath.Clamp(pos.Y, 0 + eps, 1 - eps));
			}

			public override void Render() {
				RenderState.Push();
				RenderState.Translate(BottomLeft);
				RenderState.Scale(Size);
				RenderState.Color = selector.Color;
				shader.RenderQuad();
				RenderState.Pop();

				RenderState.Push();
				RenderState.Translate(BottomLeft);
				RenderState.Scale(Size);
				RenderState.Translate(0, selector.Color.V);
				RenderState.Scale(1, 1 / Size.Y);
				RenderState.Color = Color.Black;
				Draw.Line(0, 0, 1, 0, 6);
				RenderState.Color = Color.White;
				Draw.Line(0, 0, 1, 0, 4);
				RenderState.Pop();

				base.Render();
			}
		}

		public ColorSelector() {
			Add(new MainSelect(this));
			Add(new ValueSelect(this));
		}

		Color _color = Color.White;
		public Color Color {
			get { return _color; }
			set {
				_color = value;
				if (OnChange != null)
					OnChange.Invoke(value);
			}
		}

		public event Action<Color> OnChange;

		double valueWidth = 0.1;

		public override void Update(double dt) {
			base.Update(dt);
			Visit(elem => {
				if (elem is ValueSelect) {
					elem.Size = new Vec2(Size.X * valueWidth, Size.Y * (1 - valueWidth));
					elem.Anchor = elem.Origin = new Vec2(1, 1);
				}
				if (elem is MainSelect) {
					elem.Size = new Vec2(Size.X * (1 - valueWidth), Size.Y * (1 - valueWidth));
					elem.Anchor = elem.Origin = new Vec2(0, 1);
				}
			});
		}

		public override void Render() {
			BorderColor = Color.Black;
			Draw.Rect(BottomLeft, TopRight, Color);
			base.Render();
		}

	}

}
