using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdaTool {
	internal struct SettingChoice {
		private readonly String Code;
		private readonly String Setting;
		private readonly dynamic Value;

		public void Write() {
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" [" + this.Code + "] ");
			Console.ResetColor();
			Console.Write(this.Setting + " := ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(this.Value);
			Console.ResetColor();
		}

		public void WriteLine() {
			Write();
			Console.WriteLine();
		}

		internal SettingChoice(String Code, String Setting, dynamic Value) {
			this.Code = Code;
			this.Setting = Setting;
			this.Value = Value;
		}
	}
}
