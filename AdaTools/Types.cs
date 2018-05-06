using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a collection of types
	/// </summary>
	/// <remarks>
	/// <para>This is used to hold all the types found within a unit, or within a project. Collecting them all enables easy updating of types when they are listed twice, for example, with private implementations of publicly visible types.</para>
	/// </remarks>
	public class Types {

		private List<Type> Collection;

		public Types() {

		}

		public Types(params Type[] Types) {
			this.Collection = new List<Type>(Types);
		}

		public Types(List<Type> Types) {
			this.Collection = Types;
		}
	}
}
