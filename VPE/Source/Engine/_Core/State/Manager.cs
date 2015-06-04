using System;
using System.Collections.Generic;

namespace VitPro.Engine {

	partial class State {

		/// <summary>
		/// Set the state to change this one.
		/// </summary>
		public State NextState { private get; set; }

		Stack<State> StateStack = new Stack<State>();

		/// <summary>
		/// Pushes the state to the stack of state manager.
		/// </summary>
		/// <param name="state">State.</param>
		public void PushState(State state) {
			StateStack.Push(state);
		}

		/// <summary>
		/// State manager.
		/// </summary>
		public class Manager : State {

			/// <summary>
			/// Gets current state.
			/// </summary>
			/// <value>Current state.</value>
			public State CurrentState { get; private set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="VitPro.Engine.State+Manager"/> class.
			/// </summary>
			/// <param name="states">Initial state stack.</param>
			public Manager(params State[] states) {
				for (int i = states.Length - 1; i >= 0; i--)
					ManagedStack.Push(states[i]);
			}

			/// <summary>
			/// Handles key down event.
			/// </summary>
			/// <param name="key">Key pressed.</param>
			public override void KeyDown(Key key) {
				base.KeyDown(key);
				if (CurrentState != null)
					CurrentState.KeyDown(key);
			}

			/// <summary>
			/// Handles key up event.
			/// </summary>
			/// <param name="key">Key released.</param>
			public override void KeyUp(Key key) {
				base.KeyUp(key);
				if (CurrentState != null)
					CurrentState.KeyUp(key);
			}

			/// <summary>
			/// Handles mouse button down event.
			/// </summary>
			/// <param name="button">Mouse button pressed.</param>
			/// <param name="position">Mouse position.</param>
			public override void MouseDown(MouseButton button, Vec2 position) {
				base.MouseDown(button, position);
				if (CurrentState != null)
					CurrentState.MouseDown(button, position);
			}

			/// <summary>
			/// Handles mouse button up event.
			/// </summary>
			/// <param name="button">Mouse button released.</param>
			/// <param name="position">Mouse position.</param>
			public override void MouseUp(MouseButton button, Vec2 position) {
				base.MouseUp(button, position);
				if (CurrentState != null)
					CurrentState.MouseUp(button, position);
			}

			/// <summary>
			/// Handles mouse wheel event.
			/// </summary>
			/// <param name="delta">Delta.</param>
			public override void MouseWheel(double delta) {
				base.MouseWheel(delta);
				if (CurrentState != null)
					CurrentState.MouseWheel(delta);
			}

			/// <summary>
			/// Handles mouse move event.
			/// </summary>
			/// <param name="position">Mouse position.</param>
			public override void MouseMove(Vec2 position) {
				base.MouseMove(position);
				if (CurrentState != null)
					CurrentState.MouseMove(position);
			}

			/// <summary>
			/// Render this instance.
			/// </summary>
			public override void Render() {
				base.Render();
				if (CurrentState != null)
					CurrentState.Render();
			}

			State prevState = null;

			/// <summary>
			/// Update the state.
			/// </summary>
			/// <param name="dt">Time since last update.</param>
			public override void Update(double dt) {
				base.Update(dt);
				while (ManagedStack.Count != 0) {
					if (ManagedStack.Peek().Closed)
						ManagedStack.Pop();
					else if (ManagedStack.Peek().NextState != null)
						ManagedStack.Push(ManagedStack.Pop().NextState);
					else
						break;
				}
				CurrentState = ManagedStack.Count == 0 ? null : ManagedStack.Peek();
				if (CurrentState != null) {
					CurrentState.Update(dt);
					while (CurrentState.StateStack.Count != 0)
						PushState(CurrentState.StateStack.Pop());
				} else {
					Close();
				}

				if (CurrentState != prevState) {
					StateChanged();
					prevState = CurrentState;
				}
			}

			/// <summary>
			/// Handles the state change event.
			/// </summary>
			public virtual void StateChanged() {}

			/// <summary>
			/// Set the state to change current one.
			/// </summary>
			/// <value>Next state.</value>
			public new State NextState {
				set {
					if (ManagedStack.Count != 0)
						ManagedStack.Pop();
					PushState(value);
				}
			}

			/// <summary>
			/// Pushes the state to the stack of this manager.
			/// </summary>
			/// <param name="state">State.</param>
			public new void PushState(State state) {
				ManagedStack.Push(state);
			}

			Stack<State> ManagedStack = new Stack<State>();

		}

	}

}
