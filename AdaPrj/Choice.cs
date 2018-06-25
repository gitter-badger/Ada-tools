using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPrj {
	internal struct Choice {
		private readonly String Code;
		private readonly String Name;

		public void Write() {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" [" + this.Code + "] ");
			Console.ResetColor();
			Console.Write(this.Name);
		}

		public void WriteLine() {
			Write();
			Console.WriteLine();
		}

		internal Choice(String Code, String Name) {
			this.Code = Code;
			this.Name = Name;
		}
	}
}
