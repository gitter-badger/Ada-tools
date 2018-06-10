using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	[Serializable]
	public class InvalidSourceSectionException : Exception {
		public InvalidSourceSectionException() { }
		public InvalidSourceSectionException(string message) : base(message) { }
		public InvalidSourceSectionException(string message, Exception inner) : base(message, inner) { }
		protected InvalidSourceSectionException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
