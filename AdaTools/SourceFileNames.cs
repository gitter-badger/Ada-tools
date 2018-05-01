using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public class SourceFileNames {
		public SpecFileName SpecFileName;
		public BodyFileName BodyFileName;
		public SubunitFileName SubunitFileName;

		public override String ToString() => this;

		public static implicit operator String(SourceFileNames SourceFileNames) {
			if (SourceFileNames.SpecFileName is null && SourceFileNames.BodyFileName is null && SourceFileNames.SubunitFileName is null) {
				return "Default";
			} else {
				String Result = "";
				if (SourceFileNames.SpecFileName != null) Result += SourceFileNames.SpecFileName + " ";
				if (SourceFileNames.BodyFileName != null) Result += SourceFileNames.BodyFileName + " ";
				if (SourceFileNames.SubunitFileName != null) Result += SourceFileNames.SubunitFileName;
				return Result;
			}
		}

		public SourceFileNames() {
			this.SpecFileName = null;
			this.BodyFileName = null;
			this.SubunitFileName = null;
		}
	}
}
