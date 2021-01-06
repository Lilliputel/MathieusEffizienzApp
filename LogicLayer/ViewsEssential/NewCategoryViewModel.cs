using DataLayer;
using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Stores;
using ModelLayer.Classes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewCategoryViewModel : ContentValidationViewModel<Category> {

		#region private fields
		private bool _Editing = false;
		private readonly AlertStore _AlertService;
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
					_ = _Editing ?
					_DataService.Update( new UserText(
								Title!,
								Description,
								Color.FromName( SelectedColorName! ) ) ) :
					_DataService.Insert( new Category(
						new UserText(
								Title!,
								Description,
								Color.FromName( SelectedColorName! ) ) ) );
					_DataService.Save();
					Clear();
					_AlertService.ObjektErstellt( nameof( Category ), Title! );
				},
				parameter => NoErrors );
		#endregion

		#region constructor
		public NewCategoryViewModel( IRepository dataService, AlertStore alertService ) {
			_DataService = dataService;
			_AlertService = alertService;
			ErrorsChanged += OnErrorsChanged;
			CategoryList = new ListCollectionView( _DataService.LoadAll() );
		}
		#endregion

		#region public methods
		public override bool Clear() {
			_Editing = false;
			try {
				Title = null;
				Description = null;
				SelectedColorName = null;
				return true;
			}
			catch( Exception ) {
				return false;
			}
		}
		public override bool Fill( Category item ) {
			_Editing = true;
			try {
				Title = item.UserText.Title;
				Description = item.UserText.Description;
				SelectedColorName = item.UserText.Color.Name;
				return true;
			}
			catch( Exception ) {
				Clear();
				return false;
			}
		}
		#endregion

		#region private methods
		[SuppressPropertyChangedWarnings]
		private void OnErrorsChanged( object? sender, DataErrorsChangedEventArgs e )
			=> (SaveCategoryCommand as RelayCommand)?.RaiseCanExecuteChanged( sender );
		#endregion

	}
}
