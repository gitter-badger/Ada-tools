using System;

namespace AdaTools {

	[Serializable]
	public class ProgramNameDoesNotMatchException : Exception {
		public ProgramNameDoesNotMatchException() { }
		public ProgramNameDoesNotMatchException(String message) : base(message) { }
		public ProgramNameDoesNotMatchException(String message, Exception inner) : base(message, inner) { }
		protected ProgramNameDoesNotMatchException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
