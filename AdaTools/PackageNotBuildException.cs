[System.Serializable]
public class PackageNotBuildException : System.Exception {
	public PackageNotBuildException() { }
	public PackageNotBuildException(string message) : base(message) { }
	public PackageNotBuildException(string message, System.Exception inner) : base(message, inner) { }
	protected PackageNotBuildException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}