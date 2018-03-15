﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {

	[Serializable]
	public class NotAdaProgramException : Exception {
		public NotAdaProgramException() { }
		public NotAdaProgramException(string message) : base(message) { }
		public NotAdaProgramException(string message, Exception inner) : base(message, inner) { }
		protected NotAdaProgramException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}