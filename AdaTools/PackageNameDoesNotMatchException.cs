using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {

	[Serializable]
	public class PackageNameDoesNotMatchException : Exception {
		public PackageNameDoesNotMatchException() { }
		public PackageNameDoesNotMatchException(string message) : base(message) { }
		public PackageNameDoesNotMatchException(string message, Exception inner) : base(message, inner) { }
		protected PackageNameDoesNotMatchException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
