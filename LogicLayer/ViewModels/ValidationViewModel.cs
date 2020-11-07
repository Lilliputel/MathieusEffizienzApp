using LogicLayer.ViewModels;
using ModelLayer.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace LogicLayer {

	public class ValidationViewModel : ViewModelBase, INotifyDataErrorInfo {

		//TODO Maybe i could optimise by Adding the validationattributes to a dictionary, instead of the GetValidationAttributes Method
		#region private fields
		//private bool _HasErrors;
		private readonly Type _VMType;
		private readonly List<Type> _ValidationAttributes = new List<Type>{
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
		private readonly List<string> _Properties = new List<string>();
		private readonly Dictionary<string, List<string>> _PropertyErrors = new Dictionary<string, List<string>>();
		#endregion

		#region public properties
		public bool NoErrors
			=> !HasErrors;
		public bool HasErrors
			=> _PropertyErrors.Values.Where( propList => propList.Count > 0 ).Any();
		#endregion

		#region Events
		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
		protected void RaiseErrorsChanged( string? propertyName )
			=> ErrorsChanged?.Invoke( this, new DataErrorsChangedEventArgs( propertyName ) );
		#endregion

		#region constructor
		public ValidationViewModel() {
			_VMType = GetType();
			_Properties.AddUniqueRange(
				_VMType.GetProperties( BindingFlags.Public | BindingFlags.Instance )
				.Where(
					prop => _ValidationAttributes
					.Where(
						attributeType => prop.IsDefined( attributeType, true ) )
					.Any() )
				.Select( prop => prop.Name )
				.ToList()
				);

			PropertyChanged += ( s, e ) => {
				if( _Properties.Contains( e.PropertyName ) )
					CollectErrors( e.PropertyName );
			};

			_Properties.ForEach( propertyName => CollectErrors( propertyName ) );
		}
		#endregion

		#region public methods
		public IEnumerable? GetErrors( string propertyName )
			=> _PropertyErrors.GetValueOrDefault( propertyName, new List<string>() );
		#endregion

		#region private methods
		private void CollectErrors( string? propertyName = "" ) {
			if( string.IsNullOrEmpty( propertyName ) )
				return;
			InitOrClearPropertyErrors( propertyName );
			var errors = GetPropertyValidationErrors( propertyName );
			if( errors.Count > 0 ) {
				AddPropertyErrors( propertyName, errors );
				RaiseErrorsChanged( propertyName );
			}
			if( NoErrors )
				RaiseErrorsChanged( null );
		}
		private void InitOrClearPropertyErrors( string propertyName ) {
			if( _PropertyErrors.ContainsKey( propertyName ) )
				_PropertyErrors[propertyName].Clear();
			else
				_PropertyErrors.Add( propertyName, new List<string>() );
		}
		private void AddPropertyErrors( string propertyName, List<string> errors )
			=> _PropertyErrors[propertyName].AddUniqueRange( errors );
		private List<string> GetPropertyValidationErrors( string propertyName ) {
			var placeholder = new List<string>();
			foreach( Type attributeType in _ValidationAttributes ) {
				var property = GetProperty( propertyName );
				var validAttr = TryGetValidationAttribute( property, attributeType );
				if( validAttr is null )
					continue;

				var result = ValidateProperty( property.GetValue( this ), validAttr );
				if( string.IsNullOrEmpty( result ) )
					continue;
				else
					placeholder.Add( result );
			}
			return placeholder;
		}
		private string? ValidateProperty( object value, ValidationAttribute validationAttribute ) {
			if( validationAttribute.IsValid( value ) )
				return null;
			else
				return validationAttribute.ErrorMessage;
		}
		//TODO I should catch the exceptions, which can be thrown by reflection
		private PropertyInfo GetProperty( string propertyName )
			=> _VMType.GetProperty( propertyName );
		private ValidationAttribute? TryGetValidationAttribute( PropertyInfo property, Type attributeType ) {
			MethodInfo? methodInfo = typeof( CustomAttributeExtensions ).GetMethod( nameof( CustomAttributeExtensions.GetCustomAttribute ), new Type[] { typeof( MemberInfo ), typeof( Type ) } );
			return methodInfo.Invoke( null, new object[] { property, attributeType } ) as ValidationAttribute;
		}

		#endregion

	}
}
