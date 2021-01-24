using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Stores;
using LogicLayer.Views;
using ModelLayer.Classes;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace LogicLayer.ViewModels {
	public class ViewModelMain : ViewModelBase {

		#region private fields
		private readonly ViewModelStore _ViewModels;
		private ICommand? _UpdateMainView;
		private ICommand? _UpdateEssView;
		#endregion

		#region public properties
		public ViewModelBase? SelectedVMMain { get; private set; }
		public ViewModelBase? SelectedVMEssential { get; private set; }
		public ICommand CommandUpdateMainView => _UpdateMainView ??=
			new RelayCommand( parameter => {
				try {
					UpdateSelectedMainViewModel( parameter as Type );
				}
				catch( Exception ) {
					Debug.WriteLine( $"Could not set {parameter} as a Main-ViewModel!" );
				}
			} );
		public ICommand CommandUpdateEssView => _UpdateEssView ??=
			 new RelayCommand( parameter => {
				 try {
					 UpdateSelectedEssentialViewModel( parameter as Type, null );
				 }
				 catch( Exception ) {
					 Debug.WriteLine( $"Could not set {parameter} as a Essential-ViewModel!" );
				 }
			 } );
		#endregion

		#region constructor
		public ViewModelMain( ViewModelStore viewModels )
			=> _ViewModels = viewModels;
		#endregion

		#region public methods
		public void UpdateSelectedEssentialViewModel( Type? vmType, object? passedObject = null ) {
			Debug.WriteLine( $"set the essential VM to {vmType?.Name}" );
			SelectedVMEssential = _ViewModels.GetViewModel( vmType );
			if( vmType == typeof( NewCategoryViewModel ) && passedObject is Category c )
				(SelectedVMEssential as IContent<Category>)?.Fill( c );
			else if( vmType == typeof( NewGoalViewModel ) && passedObject is Goal g )
				(SelectedVMEssential as IContent<Goal>)?.Fill( g );
			else if( vmType == typeof( NewDayTimeViewModel ) && passedObject is DoubleTime dt )
				(SelectedVMEssential as IContent<DoubleTime>)?.Fill( dt );
		}
		#endregion

		#region private helper methods
		private void UpdateSelectedMainViewModel( Type? vmType ) {
			Debug.WriteLine( $"set the main VM to {vmType?.Name}" );
			SelectedVMMain = _ViewModels.GetViewModel( vmType );
		}
		#endregion

	}
}
