using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {

	public abstract class SourceFileName {
		public String UnitFileName;
		public Casing Casing;
		public String DotReplacement;

		protected internal SourceFileName() {

		}

		protected SourceFileName(String UnitFileName, Casing Casing, String DotReplacement) {
			this.UnitFileName = UnitFileName;
			this.Casing = Casing;
			this.DotReplacement = DotReplacement;
		}
	}

	public sealed class SpecFileName: SourceFileName {

		public static implicit operator String(SpecFileName SpecFileName) => "Spec_File_Name(" + SpecFileName.UnitFileName + ", " + SpecFileName.Casing + ", " + SpecFileName.DotReplacement + ");";

		public SpecFileName() {
			this.UnitFileName = "*.ads";
			this.Casing = Casing.Mixedcase;
			this.DotReplacement = ".";
		}

		public SpecFileName(String UnitFileName, Casing Casing, String DotReplacement) : base(UnitFileName, Casing, DotReplacement) {
		}
	}

	public sealed class BodyFileName: SourceFileName {

		public static implicit operator String(BodyFileName BodyFileName) => "Body_File_Name(" + BodyFileName.UnitFileName + ", " + BodyFileName.Casing + ", " + BodyFileName.DotReplacement + ");";

		public BodyFileName() {
			this.UnitFileName = "*.adb";
			this.Casing = Casing.Mixedcase;
			this.DotReplacement = ".";
		}

		public BodyFileName(String UnitFileName, Casing Casing, String DotReplacement) : base(UnitFileName, Casing, DotReplacement) {
		}
	}

	public sealed class SubunitFileName: SourceFileName {

		public static implicit operator String(SubunitFileName SubunitFileName) => "Subunit_File_Name(" + SubunitFileName.UnitFileName + ", " + SubunitFileName.Casing + ", " + SubunitFileName.DotReplacement + ");";

		public SubunitFileName() {
			this.UnitFileName = "*.adb";
			this.Casing = Casing.Mixedcase;
			this.DotReplacement = ".";
		}

		public SubunitFileName(String UnitFileName, Casing Casing, String DotReplacement): base(UnitFileName, Casing, DotReplacement) {
		}
	}

}
