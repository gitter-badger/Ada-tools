using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a project configuration unit
	/// </summary>
	public sealed class ConfigurationUnit : Unit {

		public override Boolean HasBody {
			get => false;
			protected set { }
		}

		public override Boolean HasSpec {
			get => false;
			protected set { }
		}

		public override Boolean IsAllCallsRemote {
			get => false;
			protected set { }
		}

		public override Boolean IsPure {
			get => false;
			protected set { }
		}

		public override Boolean IsRemoteCallInterface {
			get => false;
			protected set { }
		}

		public override String[] GetFiles() => new String[] { this.Name + Extension };

		public ConfigurationUnit() : base("Gnat") {
			Source ConfigSource = new Source("Gnat" + Extension);
		}

		/// <summary>
		/// The file extension used for configuration units
		/// </summary>
		public static String Extension = ".adc";

	}
}
