using System;
using System.Collections.Generic;
using System.Text;

namespace Cmdline {
	internal struct PragmaChoice {
		internal readonly String Code;
		internal readonly String Pragma;
		internal readonly dynamic Value;

		internal PragmaChoice(String Code, String Pragma, dynamic Value) {
			this.Code = Code;
			this.Pragma = Pragma;
			this.Value = Value;
		}
	}
}
