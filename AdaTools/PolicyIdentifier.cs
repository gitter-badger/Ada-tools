namespace AdaTools {
	/// <summary>
	/// Represents the various assertion policy levels
	/// </summary>
	public enum PolicyIdentifier {
		/// <summary>
		/// Assertions are enabled
		/// </summary>
		Check,
		/// <summary>
		/// Assertions are disabled, like ignore, but not even checked
		/// </summary>
		Disable,
		/// <summary>
		/// Assertions are ignored, like disable, but still checked for correctness
		/// </summary>
		Ignore,
		/// <summary>
		/// Assertion enabled by default but supressible with -gnatp
		/// </summary>
		/// <remarks>
		/// Suppressible is only applicable for specific assertion kinds, and not as an overall policy
		/// </remarks>
		Suppressible,
	}
}
