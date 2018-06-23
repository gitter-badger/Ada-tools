using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {

	[Serializable]
	public class MissingGNATProgramException : Exception {
		public MissingGNATProgramException() { }
		public MissingGNATProgramException(String message) : base(message) { }
		public MissingGNATProgramException(String message, Exception inner) : base(message, inner) { }
		protected MissingGNATProgramException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
