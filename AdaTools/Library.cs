using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Holds all package units not part of a project
	/// </summary>
	/// <remarks>
	/// This is used for holding references to all units that are part of installed packages, and as such, would not belong to the project
	/// </remarks>
	public static class Library {

		/// <summary>
		/// Holds the actual units registered with the library
		/// </summary>
		/// <remarks>
		/// This automatically includes a definition for Standard, which doesn't actually exist as code
		/// </remarks>
		public static readonly HashSet<PackageUnit> Units = new HashSet<PackageUnit> {
			PackageUnit.Standard()
		};

		public static Boolean Contains(PackageUnit Unit) => Units.Contains(Unit);

		public static Boolean Contains(String Name) {
			foreach (Unit Unit in Units) {
				if (Name.ToUpper() == Unit.Name.ToUpper()) return true;
			}
			return false;
		}

		public static PackageUnit Lookup(PackageUnit Unit) {
			foreach (PackageUnit U in Units) {
				if (U == Unit) return U;
			}
			return null;
		}

		public static PackageUnit Lookup(String Name) {
			foreach (PackageUnit Unit in Units) {
				if (Name.ToUpper() == Unit.Name.ToUpper()) return Unit;
			}
			return null;
		}

		public static void Register(PackageUnit Unit) {
			if (Unit is null) return;
			if (!Contains(Unit)) {
				Units.Add(Unit);
			}
		}

	}
}
