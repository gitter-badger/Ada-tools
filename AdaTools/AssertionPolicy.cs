using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents the configured Assertion Policy
	/// </summary>
	public sealed class AssertionPolicy {
		/// <summary>
		/// Whether the assertion policy applies globally and how it is set
		/// </summary>
		/// <remarks>
		/// If this is null, it is understood that the assertion policy is not globally applied.
		/// </remarks>
		public readonly PolicyIdentifier? GlobalPolicy;

		/// <summary>
		/// The full list of policies to apply
		/// </summary>
		public readonly Dictionary<String, PolicyIdentifier> Policies;

		public static implicit operator PolicyIdentifier?(AssertionPolicy Policy) => Policy.GlobalPolicy;

		public static implicit operator AssertionPolicy(PolicyIdentifier Identifier) => new AssertionPolicy(Identifier);

		public static implicit operator Dictionary<String, PolicyIdentifier>(AssertionPolicy Policy) => Policy.Policies;

		public override String ToString() {
			if (this.GlobalPolicy != null) {
				return this.GlobalPolicy.ToString();
			} else {
				String Result = "";
				foreach (KeyValuePair<String, PolicyIdentifier> Policy in this.Policies) {
					Result += Policy.Key + " => " + Policy.Value + ", ";
				}
				return Result;
			}
		}

		public AssertionPolicy(PolicyIdentifier GlobalPolicy) {
			this.GlobalPolicy = GlobalPolicy;
			this.Policies = null;
		}

		public AssertionPolicy(Dictionary<String, PolicyIdentifier> Policies) {
			this.GlobalPolicy = null;
			this.Policies = Policies;
		}

	}
}
