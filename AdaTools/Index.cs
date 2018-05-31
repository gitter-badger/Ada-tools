using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an array index
	/// </summary>
	public abstract class Index {

		public override abstract String ToString(); // This weird looking signature requires that ToString be defined on anything inheriting Index

		protected Index() {

		}

	}
}
