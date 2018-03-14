using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {

	[Serializable]
	public class ProgramNameDoesNotMatchException : Exception {
		public ProgramNameDoesNotMatchException() { }
		public ProgramNameDoesNotMatchException(string message) : base(message) { }
		public ProgramNameDoesNotMatchException(string message, Exception inner) : base(message, inner) { }
		protected ProgramNameDoesNotMatchException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
