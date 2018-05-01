using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {

	[Serializable]
	public class CircularDependencyException : Exception {
		public CircularDependencyException() { }
		public CircularDependencyException(string message) : base(message) { }
		public CircularDependencyException(string message, Exception inner) : base(message, inner) { }
		protected CircularDependencyException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
