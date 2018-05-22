namespace AdaTools {
	/// <summary>
	/// Represents the various options for the -march flag
	/// </summary>
	/// <remarks>
	/// <para>This is not exhaustive, and is in fact quite limited in what is defined</para>
	/// <para>This is deliberately all lowercase, as it hopefully makes it more obvious it's the GCC flag</para>
	/// </remarks>
	public enum march {
		generic,
		native,
	}
}