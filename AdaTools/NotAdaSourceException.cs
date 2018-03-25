using System;

namespace AdaTools {

	[Serializable]
	public class NotAdaSourceException : Exception {
		public NotAdaSourceException() { }
		public NotAdaSourceException(String message) : base(message) { }
		public NotAdaSourceException(String message, Exception inner) : base(message, inner) { }
		protected NotAdaSourceException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
