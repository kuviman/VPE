using System;
using System.Collections.Generic;

namespace VitPro {
	
	public class PosGroup<T> : ICollection<T> where T : class {
		
		HashSet<T>[,] map;
		HashSet<T> spare = new HashSet<T>();
		double MinX, MinY, MaxX, MaxY, StepX, StepY;
		Dictionary<T, Vec2i> where = new Dictionary<T, Vec2i>();

		public PosGroup(double minx, double miny, double maxx, double maxy, double stepx = 1, double stepy = 1) {
			if (minx > maxx)
				GUtil.Swap(ref minx, ref maxx);
			if (miny > maxy)
				GUtil.Swap(ref miny, ref maxy);
			int sx = GMath.Ceil((maxx - minx) / stepx);
			int sy = GMath.Ceil((maxy - miny) / stepy);
			MinX = minx;
			MinY = miny;
			MaxX = maxx;
			MaxY = maxy;
			StepX = stepx;
			StepY = stepy;
			map = new HashSet<T>[sx, sy];
			for (int i = 0; i < sx; i++)
				for (int j = 0; j < sy; j++)
					map[i, j] = new HashSet<T>();
		}

		public void Add(T item, Vec2 pos) {
			Add(item, pos.X, pos.Y);
		}
		public void Add(T item, double x, double y) {
			Remove(item);
			int i = GMath.Floor((x - MinX) / StepX);
			int j = GMath.Floor((y - MinY) / StepY);
			if (i < 0 || j < 0 || i >= map.GetLength(0) || j >= map.GetLength(1)) {
					where[item] = new Vec2i(-1, -1);
				spare.Add(item);
			} else {
				where[item] = new Vec2i(i, j);
				map[i, j].Add(item);
			}
		}

		public IEnumerable<T> Query(Vec2 a, Vec2 b) {
			return Query(a.X, a.Y, b.X, b.Y);
		}
		public IEnumerable<T> Query(double minx, double miny, double maxx, double maxy) {
			if (minx > maxx)
				GUtil.Swap(ref minx, ref maxx);
			if (miny > maxy)
				GUtil.Swap(ref miny, ref maxy);
			int i1 = Math.Max(0, GMath.Floor((minx - MinX) / StepX));
			int i2 = Math.Min(map.GetLength(0), GMath.Ceil((maxx - MinX) / StepX));
			int j1 = Math.Max(0, GMath.Floor((miny - MinY) / StepY));
			int j2 = Math.Min(map.GetLength(1), GMath.Ceil((maxy - MinY) / StepY));
			for (int i = i1; i < i2; i++)
				for (int j = j1; j < j2; j++)
					foreach (var t in map[i, j])
						yield return t;
			foreach (var t in spare)
				yield return t;
		}

		public void Add(T item) {
			Add(item, MinX - 1, MinY - 1);
		}

		public void Clear() {
			spare.Clear();
			for (int i = 0; i < map.GetLength(0); i++)
				for (int j = 0; j < map.GetLength(1); j++)
					map[i, j].Clear();
				where.Clear();
		}

		public bool Contains(T item) {
			return where.ContainsKey(item);
		}

		public void CopyTo(T[] array, int arrayIndex) {
			foreach (var t in this)
				array[arrayIndex++] = t;
		}

		public int Count {
			get { return where.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(T item) {
			if (!Contains(item))
				return false;
			Vec2i pos = where[item];
			if (pos.X < 0)
				spare.Remove(item);
			else
				map[pos.X, pos.Y].Remove(item);
			return true;
		}

		public IEnumerator<T> GetEnumerator() {
			foreach (var x in where)
				yield return x.Key;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

	}

}
