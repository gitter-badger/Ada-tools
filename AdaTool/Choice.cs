using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace AdaTool {
	internal struct Choice {
		private readonly String Code;
		private readonly String Name;

		public void Write() {
			Console.Write(" [" + this.Code + "] ", Color.Khaki);
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
