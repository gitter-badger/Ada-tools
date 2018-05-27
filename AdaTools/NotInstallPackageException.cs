using System;

namespace AdaTools {

	[System.Serializable]
	public class NotInstallPackageException : System.Exception {
		public NotInstallPackageException() { }
		public NotInstallPackageException(String message) : base(message) { }
		public NotInstallPackageException(String message, System.Exception inner) : base(message, inner) { }
		protected NotInstallPackageException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}

}