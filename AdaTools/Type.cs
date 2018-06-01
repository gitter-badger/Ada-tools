using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an abstract type
	/// </summary>
	public abstract class Type {

		/// <summary>
		/// The name of the type
		/// </summary>
		public readonly String Name;

		/// <summary>
		/// Whether the type is publicly visible
		/// </summary>
		public Boolean? PubliclyVisible { get; protected set; }

		/// <summary>
		/// Whether the type's definition is private
		/// </summary>
		public Boolean? PrivateDefinition { get; protected set; }

		/// <summary>
		/// Join the specified <paramref name="Type"/> to this one
		/// </summary>
		/// <remarks>
		/// This method ensures the type can actually be joined, and throws an exception if they mismatch.
		/// </remarks>
		/// <param name="Type">The type to join</param>
		public virtual void Join(Type Type) {
			if (this != Type) throw new TypeMismatchException();
			if (this.PubliclyVisible is null) this.PubliclyVisible = Type.PubliclyVisible;
			if (this.PrivateDefinition is null) this.PrivateDefinition = Type.PrivateDefinition;
		}

		public override Boolean Equals(Object obj) => (obj is Type) && this == (Type)obj;

		public override Int32 GetHashCode() => base.GetHashCode();

		public static Boolean operator ==(Type Left, Type Right) => Left.Name.ToUpper() == Right.Name.ToUpper();

		public static Boolean operator !=(Type Left, Type Right) => Left.Name.ToUpper() != Right.Name.ToUpper();

		protected Type(String Name) {
			this.Name = Name;
			TypesLibrary.Register(this);
		}

		protected Type(String Name, Boolean? PubliclyVisible, Boolean? PrivateDefinition) : this(Name) {
			this.PubliclyVisible = PubliclyVisible;
			this.PrivateDefinition = PrivateDefinition;
		}
	}
}
