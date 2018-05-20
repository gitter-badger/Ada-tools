using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an elementary type
	/// </summary>
	public abstract class ElementaryType : Type {
		protected ElementaryType(String Name) : base(Name) {

		}
	}
}
