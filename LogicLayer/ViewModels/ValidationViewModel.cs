using LogicLayer.ViewModels;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LogicLayer {

	public abstract class ValidationViewModel : ViewModelBase, INotifyDataErrorInfo {

		#region private fields
		private static List<PropertyInfo>? _PropertyInfos;
		private readonly Dictionary<string, List<string>> _PropertyErrors = new Dictionary<string, List<string>>();
		#endregion

		#region public properties
		[AlsoNotifyFor(nameof(HasErrors))]
		public bool HasErrors
			=> _PropertyErrors.Any();
		public bool IsOkay
			=> HasErrors is false;
		#endregion

		#region public events
		public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
		private void OnErrorsChanged( string propertyName ) {
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		}
		#endregion

		#region protected Properties
		[AlsoNotifyFor(nameof(HasErrors))]
		protected List<PropertyInfo> Properties
			=> _PropertyInfos
			??= GetType()
			.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(
				prop => ValidationAttributes.Where(
					attr => prop.IsDefined(attr.GetType(), true))
				.Any())
			.ToList();
		#endregion

		#region abstract Properties
		protected abstract List<(ValidationAttribute validAttr, Predicate<object> ValidationCheck)> ValidationAttributes { get; }
		#endregion

		#region public methods
		public IEnumerable? GetErrors( [CallerMemberName] string propertyName = "" ) {
			CollectErrors();
			return _PropertyErrors.GetValueOrDefault(propertyName, new List<string>());
		}
		#endregion

		#region private methods
		private void CollectErrors() {
			_PropertyErrors.Clear();
			Properties.ForEach(
				prop => {
					ValidationAttributes.ForEach(
						( (ValidationAttribute attr, Predicate<object> check) tuple ) => {
							Type attributeType = tuple.attr.GetType();
							MethodInfo methodInfo = typeof(PropertyInfo).GetMethod("GetCustomAttribute");
							ValidationAttribute? validationAttribute = methodInfo.MakeGenericMethod(attributeType).Invoke(null, null) as ValidationAttribute;
							bool CheckResult = tuple.check( prop.GetValue(this) );
							if( validationAttribute is ValidationAttribute vattr && CheckResult is true )
								AddError(prop.Name, vattr.ErrorMessage ?? string.Empty);
						}
					);
				}
			);
			OnPropertyChanged(nameof(HasErrors));
			OnPropertyChanged(nameof(IsOkay));
		}
		private void AddError( string propertyName, string? errorMessage = "" ) {
			if( string.IsNullOrEmpty(errorMessage) )
				return;
			if( _PropertyErrors.ContainsKey(propertyName) is false )
				_PropertyErrors.Add(propertyName, new List<string>());
			if( _PropertyErrors[propertyName].Contains(errorMessage) )
				return;
			_PropertyErrors[propertyName].Add(errorMessage);
			OnErrorsChanged(propertyName);
		}
		#endregion
	}


	class PersonViewModel {

		[Required(AllowEmptyStrings = false, ErrorMessage = "First name must not be empty.")]
		[MaxLength(20, ErrorMessage = "Maximum of 50 characters is allowed.")]
		public string Firstname { get; set; }

		/// <summary>
		/// The lastname of the person.
		/// </summary>
		[Required(AllowEmptyStrings = false, ErrorMessage = "Last name must not be empty.")]
		public string Lastname { get; set; }

	}
}
