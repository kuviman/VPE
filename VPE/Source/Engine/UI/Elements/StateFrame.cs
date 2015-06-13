using System;

namespace VitPro.Engine.UI {

	/// <summary>
	/// UI Frame with a state.
	/// </summary>
	public class StateFrame : Element {

		VitPro.Engine.State state;

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.StateFrame"/> class.
		/// </summary>
		/// <param name="state">State.</param>
		public StateFrame(VitPro.Engine.State state) {
			this.state = new VitPro.Engine.State.Manager(state);
			Focusable = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.StateFrame"/> class.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public StateFrame(VitPro.Engine.State state, double width, double height) : this(state) {
			Size = new Vec2(width, height);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VitPro.Engine.UI.StateFrame"/> class.
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="size">Size.</param>
		public StateFrame(VitPro.Engine.State state, Vec2 size) : this(state) {
			Size = size;
		}

		/// <summary>
		/// Handles mouse down events.
		/// </summary>
		/// <param name="button">Button pressed.</param>
		/// <param name="position">Position.</param>
		public override void MouseDown(MouseButton button, Vec2 position) {
			base.MouseDown(button, position);
			if (state != null)
				state.MouseDown(button, MousePos(position));
		}

		/// <summary>
		/// Handles mouse move events.
		/// </summary>
		/// <param name="position">Mouse position.</param>
		public override void MouseMove(Vec2 position) {
			base.MouseMove(position);
			if (state != null)
				state.MouseMove(MousePos(position));
		}

		/// <summary>
		/// Handles mouse up events.
		/// </summary>
		/// <param name="button">Button released.</param>
		/// <param name="position">Position.</param>
		public override void MouseUp(MouseButton button, Vec2 position) {
			base.MouseUp(button, position);
			if (state != null)
				state.MouseUp(button, MousePos(position));
		}

		/// <summary>
		/// Handles key down event.
		/// </summary>
		/// <param name="key">Key pressed.</param>
		public override void KeyDown(Key key) {
			base.KeyDown(key);
			if (state != null)
				state.KeyDown(key);
		}

		/// <summary>
		/// Handles key repeat event.
		/// </summary>
		/// <param name="key">Key repeated.</param>
		public override void KeyRepeat(Key key) {
			base.KeyRepeat(key);
			if (state != null)
				state.KeyRepeat(key);
		}

		/// <summary>
		/// Handles key up event.
		/// </summary>
		/// <param name="key">Key released.</param>
		public override void KeyUp(Key key) {
			base.KeyUp(key);
			if (state != null)
				state.KeyUp(key);
		}

		/// <summary>
		/// Handles character input event.
		/// </summary>
		/// <param name="c">Character input.</param>
		public override void CharInput(char c) {
			base.CharInput(c);
			if (state != null)
				state.CharInput(c);
		}

		/// <summary>
		/// Update this element.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public override void Update(double dt) {
			base.Update(dt);
			if (state != null)
				state.Update(dt);
		}

		static Vec2i getPos(Vec2 v) {
			var result = OpenTK.Vector4d.Transform(new OpenTK.Vector4d(v.X, v.Y, 0, 1),
				RenderState.ProjectionMatrix.mat * RenderState.ModelMatrix.mat);
			return new Vec2i((int)(result.X * RenderState.Width + RenderState.Width) / 2,
				(int)(result.Y * RenderState.Height + RenderState.Height) / 2);
		}

		Vec2i renderSize;

		Vec2 MousePos(Vec2 pos) {
			return Vec2.CompMult(Vec2.CompDiv(pos - BottomLeft, Size), renderSize);
		}

		/// <summary>
		/// Render this element.
		/// </summary>
		public override void Render() {
			if (state != null) {
				Vec2i p1 = getPos(BottomLeft);
				Vec2i p2 = getPos(TopRight);
				renderSize = p2 - p1;
				RenderState.BeginArea(p1, renderSize);
				state.Render();
				RenderState.EndArea();
			}
			base.Render();
		}

	}

}
