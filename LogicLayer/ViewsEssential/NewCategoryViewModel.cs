using LogicLayer.Commands;
using LogicLayer.Manager;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
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
		[Required(AllowEmptyStrings = false, ErrorMessage = "First name must not be empty!")]
		public string? Title { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Title must not be empty!")]
		public string? Description { get; set; }
		public List<string> ColorNameList {
			get {
				var allColors = new List<string>();
				PropertyInfo[] propertyInfos = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public );
				foreach( PropertyInfo propertyInfo in propertyInfos ) {
					allColors.Add(propertyInfo.Name);
				}
				return allColors;
			}
		}
		[Required(AllowEmptyStrings = false, ErrorMessage = "Color has to be selected!")]
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
				obj => HasErrors is false);
		#endregion

		#region constructor
		public NewCategoryViewModel( IAccountableParent<Category> categoryList ) {
			CategoryList = categoryList;
			base.PropertyChanged += t;
		}
		private void t( object sender, PropertyChangedEventArgs e ) {
			if( e.PropertyName is nameof(HasErrors) ) {
				( (RelayCommand)SaveCategoryCommand ).RaiseCanExecuteChanged();
			}
			if( e.PropertyName != nameof(SaveCategoryCommand) )
				RaisePropertyChanged(nameof(SaveCategoryCommand));
		}
		#endregion

		#region methods

		#endregion

	}
}
