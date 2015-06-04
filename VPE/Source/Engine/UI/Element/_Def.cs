using System;
using System.Collections.Generic;

namespace VitPro.Engine.UI {

	/// <summary>
	/// UI element.
	/// </summary>
	public partial class Element {

        /// <summary>
        /// Initializes new UI element.
        /// </summary>
        public Element() {
            InitPositioning();
			InitRendering();
			InitText();
        }

		List<Element> children = new List<Element>();

		/// <summary>
		/// Gets the children elements.
		/// </summary>
		/// <value>The children.</value>
		public IEnumerable<Element> Children { get { return children; } }

		/// <summary>
		/// Gets the parent element.
		/// </summary>
		/// <value>The parent.</value>
		public Element Parent { get; private set; }

		/// <summary>
		/// Add a child to this element.
		/// </summary>
		/// <param name="child">Child.</param>
		public void Add(Element child) {
			children.Add(child);
			child.Parent = this;
		}

		/// <summary>
		/// Remove a child from this element.
		/// </summary>
		/// <param name="child">Child.</param>
		public void Remove(Element child) {
			children.Remove(child);
			child.Parent = null;
		}

		/// <summary>
		/// Update this element.
		/// </summary>
		/// <param name="dt">Time since last update.</param>
		public virtual void Update(double dt) {
			UpdatePosition();
			UpdateText();
			foreach (var child in children)
				child.Update(dt);
		}

		/// <summary>
		/// Render this element.
		/// </summary>
		public virtual void Render() {
			InternalRender();
			foreach (var child in children)
				child.Render();
			PostRender();
		}

		/// <summary>
		/// Visit the element and all its children recursively.
		/// </summary>
		/// <param name="action">Action applied to each element.</param>
		public void Visit(Action<Element> action) {
			if (action == null)
				return;
			action.Invoke(this);
			foreach (var child in Children)
				child.Visit(action);
		}

	}

}
