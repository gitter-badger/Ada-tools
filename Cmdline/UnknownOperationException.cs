using System;
using System.Collections.Generic;
using System.Text;

namespace Cmdline {

	[Serializable]
	public class UnknownOperationException : Exception {
		public UnknownOperationException() { }
		public UnknownOperationException(string message) : base(message) { }
		public UnknownOperationException(string message, Exception inner) : base(message, inner) { }
		protected UnknownOperationException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
