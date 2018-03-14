using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {

	[Serializable]
	public class NotAdaSourceException : Exception {
		public NotAdaSourceException() { }
		public NotAdaSourceException(string message) : base(message) { }
		public NotAdaSourceException(string message, Exception inner) : base(message, inner) { }
		protected NotAdaSourceException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
