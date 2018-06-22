using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTool {
	internal struct SettingChoice {
		internal readonly String Code;
		internal readonly String Setting;
		internal readonly dynamic Value;

		internal SettingChoice(String Code, String Setting, dynamic Value) {
			this.Code = Code;
			this.Setting = Setting;
			this.Value = Value;
		}
	}
}
