using System;

namespace AdaTools {

	[Serializable]
	public class PackageNameDoesNotMatchException : Exception {
		public PackageNameDoesNotMatchException() { }
		public PackageNameDoesNotMatchException(String message) : base(message) { }
		public PackageNameDoesNotMatchException(String message, Exception inner) : base(message, inner) { }
		protected PackageNameDoesNotMatchException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
