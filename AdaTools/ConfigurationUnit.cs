using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a project configuration unit
	/// </summary>
	public sealed class ConfigurationUnit : Unit {

		//! While this may appear exhaustive, it should not be assumed as such. Some things were skipped with the intent on coming back to them later.

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
		public AssumeNoInvalidValues? AssumeNoInvalidValues { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-elaboration-checks"/>
		public ElaborationChecks? ElaborationChecks { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-extensions-allowed"/>
		public ExtensionsAllowed? ExtensionsAllowed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-fast-math"/>
		public Boolean FastMath { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <see cref="https://docs.adacore.com/gnat_rm-docs/html/gnat_rm/gnat_rm/implementation_defined_pragmas.html#pragma-license"/>
		public License? License { get; set; }

		public override Boolean HasBody { get; protected set; }

		public override Boolean HasSpec { get; protected set; }

		/// <summary>
		/// Does the unit have an actual configuration file?
		/// </summary>
		/// <remarks>
		/// This property uses a little trickery to combine the behavior of HasBody and HasSpec, neither of which is particularly applicable, but not semantic nonsense either. If a downstream developer sets either property to true, HasConf will reflect this, and HasConf will set both properties accordingly. This should also help with polymorphism cases, where a view conversion doesn't have to take place to actually check/modify correctly.
		/// </remarks>
		public Boolean HasConf {
			get => this.HasBody || this.HasSpec;
			set {
				this.HasBody = value;
				this.HasSpec = value;
			}
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

		/// <summary>
		/// Save the configuration as a gnat.adc file
		/// </summary>
		public void Save() {
			using (StreamWriter Config = new StreamWriter("gnat.adc")) {
				switch (this.AdaVersion) {
					case AdaTools.AdaVersion.Ada1983:
						Config.WriteLine("pragma Ada_83;");
						break;
					case AdaTools.AdaVersion.Ada1995:
						Config.WriteLine("pragma Ada_95;");
						break;
					case AdaTools.AdaVersion.Ada2005:
						Config.WriteLine("pragma Ada_2005;");
						break;
					case AdaTools.AdaVersion.Ada2012:
						Config.WriteLine("pragma Ada_2012;");
						break;
					default:
						break;
				}
				switch (this.AssertionPolicy) {
					case AdaTools.AssertionPolicy.Check:
						Config.WriteLine("pragma Assertion_Policy(Check);");
						break;
					case AdaTools.AssertionPolicy.Disable:
						Config.WriteLine("pragma Assertion_Policy(Disable);");
						break;
					case AdaTools.AssertionPolicy.Ignore:
						Config.WriteLine("pragma Assertion_Policy(Ignore);");
						break;
					default:
						break;
				}
				switch (this.AssumeNoInvalidValues) {
					case AdaTools.AssumeNoInvalidValues.On:
						Config.WriteLine("pragma Assume_No_Invalid_Values(On);");
						break;
					case AdaTools.AssumeNoInvalidValues.Off:
						Config.WriteLine("pragma Assume_No_Invalid_Values(Off);");
						break;
					default:
						break;
				}
				switch (this.ElaborationChecks) {
					case AdaTools.ElaborationChecks.Dynamic:
						Config.WriteLine("pragma Elaboration_Checks(Dynamic);");
						break;
					case AdaTools.ElaborationChecks.Static:
						Config.WriteLine("pragma Elaboration_Checks(Static);");
						break;
					default:
						break;
				}
				switch (this.ExtensionsAllowed) {
					case AdaTools.ExtensionsAllowed.On:
						Config.WriteLine("pragma Extensions_Allowed(On);");
						break;
					case AdaTools.ExtensionsAllowed.Off:
						Config.WriteLine("pragma Extensions_Allowed(Off);");
						break;
					default:
						break;
				}
				switch (this.FastMath) {
					case true:
						Config.WriteLine("pragma Fast_Math;");
						break;
					case false:
						break;
				}
				switch (this.License) {
					case AdaTools.License.GPL:
						Config.WriteLine("pragma License(GPL);");
						break;
					case AdaTools.License.Modified_GPL:
						Config.WriteLine("pragma License(Modified_GPL);");
						break;
					case AdaTools.License.Restricted:
						Config.WriteLine("pragma License(Restricted);");
						break;
					case AdaTools.License.Unrestricted:
						Config.WriteLine("pragma License(Unrestricted);");
						break;
				}
			}
		}

		public ConfigurationUnit() : base("gnat") {
			Source ConfigSource;
			try {
				ConfigSource = new Source("gnat" + Extension);
				this.HasConf = true;
				this.AdaVersion = ConfigSource.ParseAdaVersion();
				this.AssertionPolicy = ConfigSource.ParseAssertionPolicy();
				this.AssumeNoInvalidValues = ConfigSource.ParseAssumeNoInvalidValues();
				this.ElaborationChecks = ConfigSource.ParseElaborationChecks();
				this.ExtensionsAllowed = ConfigSource.ParseExtensionsAllowed();
				this.FastMath = ConfigSource.ParseFastMath();
				this.License = ConfigSource.ParseLicense();
			} catch {
				// Don't bother doing anything. We can still create the object without any source existing
				this.HasConf = false;
			}
		}

		/// <summary>
		/// The file extension used for configuration units
		/// </summary>
		public static String Extension = ".adc";

	}
}
