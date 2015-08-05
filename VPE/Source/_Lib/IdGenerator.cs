using System;

namespace VitPro {

	public class IdGenerator {

		long nextId = 1;

		public long Next {
			get { return nextId++; }
		}

	}

}
