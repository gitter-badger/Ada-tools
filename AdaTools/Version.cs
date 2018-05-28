using System;

namespace AdaTools {
	/// <summary>
	/// Represents a version number
	/// </summary>
	/// <remarks>
	/// This is a specific type so that more information can be stored than just a simple integer or rational, but actual comparison logic can be implemented
	/// </remarks>
	public struct Version {

		public readonly UInt16 Major;

		public readonly UInt16 Minor;

		public readonly UInt16 Patch;

		public override String ToString() => this.Major.ToString() + '.' + this.Minor.ToString() + '.' + this.Patch.ToString();

		public override Boolean Equals(Object Object) =>
		(Object is Version) ?
		this == (Version)Object : false;

		public override Int32 GetHashCode() => this.Major.GetHashCode() ^ this.Minor.GetHashCode() ^ this.Patch.GetHashCode();

		public static Boolean operator ==(Version Left, Version Right) => (Left.Major == Right.Major && Left.Minor == Right.Minor && Left.Patch == Right.Patch);

		public static Boolean operator !=(Version Left, Version Right) => (Left.Major != Right.Major || Left.Minor != Right.Minor || Left.Patch != Right.Patch);

		public static Boolean operator <(Version Left, Version Right) {
			if (Left.Major < Right.Major) {
				return true;
			} else if (Left.Major == Right.Major) {
				if (Left.Minor < Right.Minor) {
					return true;
				} else if (Left.Minor == Right.Minor) {
					if (Left.Patch <= Right.Patch) {
						return true;
					}
				}
			}
			return false;
		}

		public static Boolean operator >(Version Left, Version Right) {
			if (Left.Major > Right.Major) {
				return true;
			} else if (Left.Major == Right.Major) {
				if (Left.Minor > Right.Minor) {
					return true;
				} else if (Left.Minor == Right.Minor) {
					if (Left.Patch >= Right.Patch) {
						return true;
					}
				}
			}
			return false;
		}

		public static Boolean operator <=(Version Left, Version Right) => (Left.Major <= Right.Major && Left.Minor <= Right.Minor && Left.Patch <= Right.Patch);

		public static Boolean operator >=(Version Left, Version Right) => (Left.Major >= Right.Major && Left.Minor >= Right.Minor && Left.Patch >= Right.Patch);

		public Version(UInt16 Major, UInt16 Minor, UInt16 Patch = 0) {
			this.Major = Major;
			this.Minor = Minor;
			this.Patch = Patch;
		}

		public Version(String Version) {
			String[] Split = Version.Split('.');
			this.Major = UInt16.Parse(Split[0]);
			this.Minor = UInt16.Parse(Split[1]);
			if (Split.Length >= 3) {
				this.Patch = UInt16.Parse(Split[2]);
			} else {
				this.Patch = 0;
			}
		}
	}
}