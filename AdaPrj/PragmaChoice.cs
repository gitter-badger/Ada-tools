using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace AdaPrj {
	internal struct PragmaChoice {
		private readonly String Code;
		private readonly String Pragma;
		private readonly dynamic Value;

		public void Write() {
			Console.Write(" [" + this.Code + "] ", Color.Khaki);
			Console.Write(this.Pragma + " := ");
			Console.Write((this.Value as String));
		}

		public void WriteLine() {
			Write();
			Console.WriteLine();
		}

		internal PragmaChoice(String Code, String Pragma, dynamic Value) {
			this.Code = Code;
			this.Pragma = Pragma;
			this.Value = Value;
		}
	}
}
