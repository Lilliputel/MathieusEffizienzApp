using LogicLayer.Commands;
using LogicLayer.Manager;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewCategoryViewModel : ValidationViewModel {

		#region private fields
		private IAccountableParent<Category> CategoryList;
		private ICommand? _SaveCategoryCommand;
		#endregion

		#region public properties
		[AlsoNotifyFor(nameof(SaveCategoryCommand))]
		[Required(AllowEmptyStrings = false, ErrorMessage = "First name must not be empty.")]
		public string Title { get; set; }
		[AlsoNotifyFor(nameof(SaveCategoryCommand))]
		[Required(AllowEmptyStrings = false, ErrorMessage = "First name must not be empty.")]
		public string Description { get; set; }
		public List<string> ColorNameList {
			get {
				var allColors = new List<string>();
				PropertyInfo[] propertyInfos = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public );
				foreach( PropertyInfo propertyInfo in propertyInfos ) {
					allColors.Add(propertyInfo.Name);
					//if( propertyInfo.GetValue(null, null) is Color color )
					//	allColors.Add((color, propertyInfo.Name));
				}
				return allColors;
			}
		}
		[AlsoNotifyFor(nameof(SaveCategoryCommand))]
		public string? SelectedColorName { get; set; }
		#endregion

		#region public commands
		public ICommand SaveCategoryCommand => _SaveCategoryCommand ??=
			new RelayCommand(
				parameter => {
					CategoryList.Children.Add(
						new Category(
							new UserText(
								Title,
								Description,
								Color.FromName(SelectedColorName))));
					AlertManager.ObjektErstellt(nameof(Category), Title);
				},
				_ => IsOkay);

		protected override List<(ValidationAttribute validAttr, Predicate<object> ValidationCheck)> ValidationAttributes
			=> new List<(ValidationAttribute validAttr, Predicate<object> ValidationCheck)> {
				( new StringLengthAttribute(50), new Predicate<object>((value) => string.IsNullOrEmpty(value?.ToString() ?? string.Empty)))
			};

		#endregion

		#region constructor
		public NewCategoryViewModel( IAccountableParent<Category> categoryList ) {
			CategoryList = categoryList;
			//ErrorsChanged += ( s, e ) => {
			//	SaveCategoryCommand.CanExecuteChanged?.Invoke(s, e);
			//};
#warning i have to implement a way to raise the canexecute changed event
		}
		#endregion

		#region methods

		#endregion

	}
}
