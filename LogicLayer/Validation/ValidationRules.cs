using ModelLayer.Classes;
using System.ComponentModel.DataAnnotations;

namespace LogicLayer.Validation {
	public static class ValidationRules {

		#region New Day Time
		public static ValidationResult ValidateCategory( object? validationObject, ValidationContext context )
			=> validationObject is null ? new ValidationResult( "The category has to be selected!" ) : validationObject is Category ? ValidationResult.Success : new ValidationResult( "The category has to be selected!" );
		public static ValidationResult ValidateType( object? validationObject, ValidationContext context )
			=> validationObject?.GetType().Name == context.MemberName ? ValidationResult.Success : new ValidationResult( $"The {context.DisplayName} has to be of type {context.MemberName}!" );
		#endregion
	}
}
