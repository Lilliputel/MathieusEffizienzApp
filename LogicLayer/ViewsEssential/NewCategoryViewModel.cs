using LogicLayer.Commands;
using LogicLayer.Manager;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewCategoryViewModel : ValidationViewModel {

		#region private fields
		private IAccountableParent<Category> CategoryList;
		private ICommand? _SaveCategoryCommand;
		#endregion

		#region public properties
		[Required( AllowEmptyStrings = false, ErrorMessage = "The title has to be specified!" )]
		public string? Title { get; set; }
		[Required( AllowEmptyStrings = false, ErrorMessage = "The description has to be specified!" )]
		public string? Description { get; set; }
		public List<string> ColorNameList
			=> typeof( Color ).GetProperties( BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public ).Select( prop => prop.Name ).ToList();

		[Required( AllowEmptyStrings = false, ErrorMessage = "The color has to be selected!" )]
		public string? SelectedColorName { get; set; }
		#endregion

		#region public commands
		public ICommand SaveCategoryCommand => _SaveCategoryCommand ??=
			new RelayCommand(
				parameter => {
					CategoryList.Children.Add(
						new Category(
							new UserText(
								Title!,
								Description,
								Color.FromName( SelectedColorName ) ) ) );
					AlertManager.ObjektErstellt( nameof( Category ), Title! );
				},
				parameter => NoErrors );
		#endregion

		#region constructor
		public NewCategoryViewModel( IAccountableParent<Category> categoryList ) : base() {
			CategoryList = categoryList;
			ErrorsChanged += OnErrorsChanged;
		}
		#endregion

		#region private methods
		private void OnErrorsChanged( object sender, DataErrorsChangedEventArgs e )
			=> (SaveCategoryCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
		#endregion

	}
}
