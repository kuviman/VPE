using System;
using System.Collections.Generic;

namespace VitPro {
	
	public class Group<T> : ICollection<T> where T : class {

		HashSet<T> objects = new HashSet<T>();

		struct Operation {
			
			public enum OpType {
				New,
				Remove,
				Clear,
			}

			public OpType Type;
			public T Object;

			public Operation(OpType type, T obj) {
				Type = type;
				Object = obj;
			}
		}

		Queue<Operation> ops = new Queue<Operation>();

		public void Add(T item) {
			ops.Enqueue(new Operation(Operation.OpType.New, item));
		}

		public void Clear() {
			ops.Enqueue(new Operation(Operation.OpType.Clear, null));
		}

		public bool Contains(T item) {
			return objects.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex) {
			objects.CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return objects.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(T item) {
			ops.Enqueue(new Operation(Operation.OpType.Remove, item));
			return true;
		}

		public IEnumerable<T> RemoveAll(Predicate<T> pred) {
			List<T> res = new List<T>();
			foreach(var item in this)
				if (pred(item))
				{
					Remove(item);
					res.Add(item);
				}
			return res;
		}

		public IEnumerator<T> GetEnumerator() {
			return objects.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void Refresh() {
			while (ops.Count > 0) {
				var op = ops.Dequeue();
				if (op.Type == Operation.OpType.Clear)
					objects.Clear();
				else if (op.Type == Operation.OpType.New)
					objects.Add(op.Object);
				else if (op.Type == Operation.OpType.Remove) {
					if (objects.Contains(op.Object))
						objects.Remove(op.Object);
				} else throw new Exception();
			}
		}

	}

}
