using LogicLayer.Commands;
using ModelLayer.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LogicLayer.BaseViewModels {

	public class ValidationViewModel : ViewModelBase, INotifyDataErrorInfo {

		#region private fields
		private readonly Type _VMType;
		private readonly IEnumerable<Type> _AttributeTypes = new List<Type>{
				typeof(CompareAttribute),
				typeof(DataTypeAttribute),
				typeof(MaxLengthAttribute),
				typeof(MinLengthAttribute),
				typeof(RangeAttribute),
				typeof(RegularExpressionAttribute),
				typeof(RequiredAttribute),
				typeof(StringLengthAttribute),
				typeof(CustomValidationAttribute)
			};
		private readonly IEnumerable<PropertyInfo> _Properties;
		private readonly List<IRelayCommand>? _RelayCommands;
		private readonly Dictionary<string, IEnumerable<ValidationAttribute>> _PropertyValidations;
		private readonly Dictionary<string, List<string>> _PropertyErrors = new Dictionary<string, List<string>>();
		#endregion

		#region public properties
		public bool NoErrors
			=> !HasErrors;
		public bool HasErrors
			=> _PropertyErrors.Values.Any( errorList => errorList.Count > 0 );
		#endregion

		#region public events
		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
		protected void RaiseErrorsChanged( string? propertyName ) {
			ErrorsChanged?.Invoke( this, new DataErrorsChangedEventArgs( propertyName ) );
			Debug.WriteLine( $"Raised ErrorsChanged with the Property {propertyName}" );
		}
		#endregion

		#region constructor
		public ValidationViewModel() {
			_VMType = GetType();
			_Properties = _VMType.GetProperties( BindingFlags.Public | BindingFlags.Instance );
			_RelayCommands = _Properties.Select( prop => prop.GetValue( this ) ).Where( val => val is IRelayCommand )?.Cast<IRelayCommand>().ToList();
			_PropertyValidations = new Dictionary<string, IEnumerable<ValidationAttribute>>(
					_Properties.Where( prop => _AttributeTypes.Any( attrType => prop.IsDefined( attrType, false ) ) ) // where atleast one attribute is defined
					.Select( prop => new KeyValuePair<string, IEnumerable<ValidationAttribute>>(
						prop.Name,
						_AttributeTypes.SelectMany( attrType => prop.GetCustomAttributes( attrType, false ).Cast<ValidationAttribute>() ) // get all Attributes of the specified types
						.Where( attr => attr is ValidationAttribute ) ) // filter out null values
					) );

			//Hook into the PropertyChanged Event of validated Properties
			PropertyChanged += ( sender, e ) => {
				if( e?.PropertyName is string propName && _PropertyValidations.ContainsKey( propName ) )
					CollectToPropertyErrors( propName );
			};
			//Initialize all Errors
			_PropertyValidations.Keys.ToList().ForEach( propertyName => CollectToPropertyErrors( propertyName ) );
		}
		#endregion

		#region public methods
		public IEnumerable GetErrors( string? propertyName )
			=> _PropertyErrors.GetValueOrDefault( propertyName ?? "" ) ?? new List<string>();
		#endregion

		#region private methods
		private void CollectToPropertyErrors( string? propertyName ) {
			if( string.IsNullOrEmpty( propertyName ) )
				return;

			int oldErrorsCount = 0;

			if( _PropertyErrors.ContainsKey( propertyName ) ) {
				oldErrorsCount = _PropertyErrors[propertyName].Count;
				_PropertyErrors[propertyName].Clear();
			}
			else
				_PropertyErrors.Add( propertyName, new List<string>() );

			if( GetValidationErrors( propertyName ) is List<string> errors ) {
				if( errors.Count > 0 )
					_PropertyErrors[propertyName].AddUniqueRange( errors );
				if( errors.Count != oldErrorsCount ) {
					RaiseErrorsChanged( propertyName );
					_RelayCommands?.ForEach( cmd => cmd.RaiseCanExecuteChanged( this ) );
				}
			}
		}
		private List<string>? GetValidationErrors( string propertyName )
			=> _PropertyValidations[propertyName] // get the validationAttributes
				.Select( vAttr => ValidateProperty( propertyName, vAttr ) ) // validate property with 
				.Where( err => string.IsNullOrWhiteSpace( err ) is false ) // filter out null values
				.ToList() as List<string>;
		private string? ValidateProperty( string propName, ValidationAttribute validAttribute )
			=> validAttribute is CustomValidationAttribute
			? validAttribute.GetValidationResult( GetPropertyValue( propName ), new ValidationContext( this ) { DisplayName = propName, MemberName = _VMType.GetProperty( propName )?.PropertyType?.Name ?? "" } )?.ErrorMessage
			: validAttribute.IsValid( GetPropertyValue( propName ) ) ? null : validAttribute.ErrorMessage;
		private object? GetPropertyValue( string propName )
			=> _VMType.GetProperty( propName )?.GetValue( this );
		#endregion

	}
}
