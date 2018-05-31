using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an array type
	/// </summary>
	public sealed class ArrayType : CompositeType {

		/// <summary>
		/// How the array is indexed
		/// </summary>
		public readonly Index[] Indices;

		/// <summary>
		/// What the array is of
		/// </summary>
		public readonly Type Of;

		public override String ToString() {
			String Result = "type " + this.Name + " is array(";
			foreach (Index Index in this.Indices) {
				Result += Index + ", ";
			}
			Result.TrimEnd(' ').TrimEnd(',');
			Result += ") of " + this.Of.Name + ";";
			return Result;
		}

		public ArrayType(String Name) : base(Name) {

		}

		public ArrayType(String Name, Type Of, params Index[] Indices) : this(Name) {
			this.Of = Of;
			this.Indices = Indices;
		}

	}
}
