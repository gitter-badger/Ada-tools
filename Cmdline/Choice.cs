using System;
using System.Collections.Generic;
using System.Text;

namespace Cmdline {
	internal struct Choice {
		internal readonly String Code;
		internal readonly String Name;

		public static implicit operator String(Choice Choice) => " [" + Choice.Code + "] " + Choice.Name;

		internal Choice(String Code, String Name) {
			this.Code = Code;
			this.Name = Name;
		}
	}
}
