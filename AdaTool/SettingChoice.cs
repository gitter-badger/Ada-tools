﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace AdaTool {
	internal struct SettingChoice {
		private readonly String Code;
		private readonly String Setting;
		private readonly dynamic Value;

		public void Write() {
			Console.Write(" [" + this.Code + "] ", Color.Khaki);
			Console.Write(this.Setting + " := ");
			Console.Write(this.Value);
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
