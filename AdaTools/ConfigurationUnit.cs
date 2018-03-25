using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a project configuration unit
	/// </summary>
	public sealed class ConfigurationUnit : Unit {
		
		// While this may appear exhaustive, it should not be assumed as such. Some things were skipped with the intent on coming back to them later.

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-ada-83" />
		public AdaVersion? AdaVersion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-assertion-policy" />
		public AssertionPolicy? AssertionPolicy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-assume-no-invalid-values" />
		public Boolean? AssumeNoInvalidValues { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-elaboration-checks"/>
		public ElaborationChecks? ElaborationChecks { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-extensions-allowed"/>
		public Boolean? ExtensionsAllowed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-fast-math"/>
		public Boolean? FastMath { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-license"/>
		public License? License { get; set; }

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
			this.AdaVersion = ConfigSource.ParseAdaVersion();
			this.AssertionPolicy = ConfigSource.ParseAssertionPolicy();
			this.AssumeNoInvalidValues = ConfigSource.ParseAssumeNoInvalidValues();
			this.ElaborationChecks = ConfigSource.ParseElaborationChecks();
			this.ExtensionsAllowed = ConfigSource.ParseExtensionsAllowed();
			this.FastMath = ConfigSource.ParseFastMath();
			this.License = ConfigSource.ParseLicense();
		}

		/// <summary>
		/// The file extension used for configuration units
		/// </summary>
		public static String Extension = ".adc";

	}
}
