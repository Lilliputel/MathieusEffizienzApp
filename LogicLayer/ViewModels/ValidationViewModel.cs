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

	public abstract class ValidationViewModel : ViewModelBase, INotifyDataErrorInfo {

#warning If a button gets clicked, the validation no longer works?!
#warning the ErrorsChanged Event should be Implemented seperately from the PropertyChanged, since it could get a huge performance impact, if CollectErrors always gets executed (Maybe Dictionary<propname, List<ValidationAttribute>> 

		#region private fields
		private bool _HasErrors;
		private readonly Type _VMType;
		private readonly List<Type> _ValidationAttributes= new List<Type>();
		private readonly List<PropertyInfo> _Properties = new List<PropertyInfo>();
		private readonly Dictionary<string, List<string>> _PropertyErrors = new Dictionary<string, List<string>>();
		#endregion

		#region INotifyDataErrorInfo Implementation
		public bool HasErrors {
			get {
				bool value = _PropertyErrors.Values.Where(value => value.Count > 0).Any();
				if( value != _HasErrors ) {
					_HasErrors = value;
					RaisePropertyChanged(nameof(HasErrors));
				}
				return _HasErrors;
			}
		}

		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
		protected void RaiseErrorsChanged( string? propertyName )
			=> ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		public IEnumerable? GetErrors( string propertyName ) {
			CollectErrors(propertyName);
			return _PropertyErrors.GetValueOrDefault(propertyName, new List<string>());
		}
		#endregion

		#region constructor
		public ValidationViewModel() {
			_VMType = GetType();
			_ValidationAttributes.AddUniqueRange(DefaultValidationAttributes());
			_Properties.AddUniqueRange(
				_VMType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(
					prop => _ValidationAttributes.Where(
						attributeType => prop.IsDefined(attributeType, true))
					.Any()).ToList()
				);

			_Properties.ForEach(prop => CollectErrors(prop.Name));
		}
		#endregion

		#region private methods
		private void CollectErrors( string? propertyName = "" ) {
			if( string.IsNullOrEmpty(propertyName) )
				return;

			var property = _VMType.GetProperty(propertyName);

			if( _PropertyErrors.ContainsKey(propertyName) )
				_PropertyErrors[propertyName].Clear();
			else
				_PropertyErrors.Add(propertyName, new List<string>());

			foreach( var atttributeType in _ValidationAttributes ) {

				var methodInfo = typeof(CustomAttributeExtensions).GetMethod(nameof(CustomAttributeExtensions.GetCustomAttribute), new Type[]{ typeof(MemberInfo), typeof(Type)} );
				var methodReturnValue = methodInfo.Invoke(null, new object[]{ property, atttributeType });

				if( methodReturnValue is ValidationAttribute vAttribute )
					if( vAttribute.IsValid(property.GetValue(this)) is false )
						_PropertyErrors[propertyName].Add(vAttribute.ErrorMessage);
			}
		}

		private List<Type> DefaultValidationAttributes() // I could also use reflection
			=> new List<Type>{
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
		#endregion
	}

}
