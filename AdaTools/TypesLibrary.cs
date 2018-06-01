using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Holds all types not part of a project
	/// </summary>
	/// <remarks>
	/// This is used for holding references to all types that are part of installed libraries, and as such, would not belong to the project
	/// </remarks>
	public static class TypesLibrary {

		/// <summary>
		/// Holds the actual types registered with the library
		/// </summary>
		/// <remarks>
		/// This is automatically filled with the types from Standard, but without actual definitions as those vary with platform and architecture
		/// </remarks>
		private static readonly HashSet<Type> Types = new HashSet<Type> {
			new SignedType("Short_Short_Integer"),
			new SignedType("Short_Integer"),
			new SignedType("Integer"),
			new SignedType("Long_Integer"),
			new SignedType("Long_Long_Integer"),
			new FloatType("Short_Float"),
			new FloatType("Float"),
			new FloatType("Long_Float"),
			new FloatType("Long_Long_Float"),
			new EnumerationType("Character"),
			new EnumerationType("Wide_Character"),
			new EnumerationType("Wide_Wide_Character"),
			new ArrayType("String"),
			new ArrayType("Wide_String"),
			new ArrayType("Wide_Wide_String"),
			new OrdinaryType("Duration")
		};

		public static Boolean Contains(Type Type) => Types.Contains(Type);

		public static Boolean Contains(String Name) {
			foreach (Type Type in Types) {
				if (Name.ToUpper() == Type.Name.ToUpper()) return true;
			}
			return false;
		}

		public static Type Lookup(Type Type) {
			foreach (Type T in Types) {
				if (T == Type) return T;
			}
			return null;
		}

		public static Type Lookup(String Name) {
			foreach (Type Type in Types) {
				if (Name.ToUpper() == Type.Name.ToUpper()) return Type;
			}
			return null;
		}

		public static void Register(Type Type) {
			if (Type is null) return;
			if (!Contains(Type)) {
				Types.Add(Type);
			}
		}

	}
}
