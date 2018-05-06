using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public struct Range<Type> where Type : IComparable {

		internal readonly Type Lower;

		internal readonly Type Upper;

		public Range(Type Lower, Type Upper) {
			if (Lower.CompareTo(Upper) >= 0) throw new IndexOutOfRangeException();
			this.Lower = Lower;
			this.Upper = Upper;
		}

	}
}
