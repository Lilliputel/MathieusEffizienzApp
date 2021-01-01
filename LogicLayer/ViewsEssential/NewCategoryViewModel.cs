using DataLayer;
using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Services;
using ModelLayer.Classes;
using PropertyChanged;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewCategoryViewModel : ValidationViewModel {

		#region private fields
		private readonly IRepository _DataService;
		private ICommand? _SaveCategoryCommand;
		#endregion

		#region public properties
		public ICollectionView CategoryList { get; }
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
					_DataService.Insert(
						new Category(
							new UserText(
								Title!,
								Description,
								Color.FromName( SelectedColorName! ) ) ) );
					_DataService.Save();
					NotificationService.ObjektErstellt( nameof( Category ), Title! );
				},
				parameter => NoErrors );
		#endregion

		#region constructor
		public NewCategoryViewModel( IRepository dataService ) {
			_DataService = dataService;
			ErrorsChanged += OnErrorsChanged;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
		}
		#endregion

		#region private methods
		[SuppressPropertyChangedWarnings]
		private void OnErrorsChanged( object? sender, DataErrorsChangedEventArgs e )
			=> (SaveCategoryCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
		#endregion

	}
}
