namespace AdaTools {

	[System.Serializable]
	public class NotAdaPackageException : System.Exception {
		public NotAdaPackageException() { }
		public NotAdaPackageException(string message) : base(message) { }
		public NotAdaPackageException(string message, System.Exception inner) : base(message, inner) { }
		protected NotAdaPackageException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}

}