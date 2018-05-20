[System.Serializable]
public class InvalidRangeException : System.Exception {
	public InvalidRangeException() { }
	public InvalidRangeException(string message) : base(message) { }
	public InvalidRangeException(string message, System.Exception inner) : base(message, inner) { }
	protected InvalidRangeException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}