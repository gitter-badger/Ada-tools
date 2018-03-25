using System;

namespace AdaTools {

	[Serializable]
	public class NotAdaProgramException : Exception {
		public NotAdaProgramException() { }
		public NotAdaProgramException(String message) : base(message) { }
		public NotAdaProgramException(String message, Exception inner) : base(message, inner) { }
		protected NotAdaProgramException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
