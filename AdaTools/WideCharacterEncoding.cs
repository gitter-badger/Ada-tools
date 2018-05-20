using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents the various configurations for Wide Character Encoding
	/// </summary>
	/// <remarks>
	/// Except for legacy compatability reasons, you certainly want UTF-8
	/// </remarks>
	public enum WideCharacterEncoding {
		Hex,
		Upper,
		Shift_JIS,
		EUC,
		UTF8,
		Brackets
	}
}
