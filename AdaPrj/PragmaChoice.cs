using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdaPrj {
	internal struct PragmaChoice {
		private readonly String Code;
		private readonly String Pragma;
		private readonly dynamic Value;

		public void Write() {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" [" + this.Code + "] ");
			Console.ResetColor();
			Console.Write(this.Pragma + " := ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(this.Value?.ToString() ?? "");
			Console.ResetColor();
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
