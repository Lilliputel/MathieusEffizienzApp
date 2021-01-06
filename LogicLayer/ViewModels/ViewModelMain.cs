using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Stores;
using ModelLayer.Classes;
using ModelLayer.Enums;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace LogicLayer.ViewModels {
	public class ViewModelMain : ViewModelBase {

		#region private fields
		private readonly ViewModelStore _ViewModels;
		private ICommand? _UpdateMainView;
		private ICommand? _UpdateEssView;
		private ICommand? _CreateObjects;
		#endregion

		#region public properties
		public ViewModelBase? SelectedVMMain { get; private set; }
		public ViewModelBase? SelectedVMEssential { get; private set; }
		public ICommand CommandUpdateMainView => _UpdateMainView ??=
			new RelayCommand( parameter => {
				try {
					UpdateSelectedMainViewModel( Enum.Parse<ViewModelEnum>( parameter as string ?? "" ) );
				}
				catch( ArgumentException ) {
					Debug.WriteLine( $"Could not Parse the string {parameter} to a ViewModel!" );
				}
			} );
		public ICommand CommandUpdateEssView => _UpdateEssView ??=
			 new RelayCommand( parameter => {
				 try {
					 UpdateSelectedEssentialViewModel( Enum.Parse<ViewModelEnum>( parameter as string ?? "" ), null );
				 }
				 catch( ArgumentException ) {
					 Debug.WriteLine( $"Could not Parse the string {parameter} to a ViewModel!" );
				 }
			 } );
		public ICommand CommandCreateObjects => _CreateObjects ??=
			new RelayCommand( parameter => Debug.WriteLine( "Tried to Generate Objects in the MainViewModel!" ) );
		#endregion

		#region constructor
		public ViewModelMain( ViewModelStore viewModels )
			=> _ViewModels = viewModels;
		#endregion

		#region public methods
		private void UpdateSelectedMainViewModel( ViewModelEnum newVMType ) {
			Debug.WriteLine( $"set the main VM to {newVMType}" );
			SelectedVMMain = _ViewModels.GetViewModel( newVMType );
		}
		public void UpdateSelectedEssentialViewModel( ViewModelEnum newVMType, object? passedObject = null ) {
			Debug.WriteLine( $"set the essential VM to {newVMType}" );
			ViewModelBase? vm = _ViewModels.GetViewModel( newVMType );
			if( newVMType == ViewModelEnum.NewCategory && passedObject is Category c )
				(vm as IContent<Category>)?.Fill( c );
			else if( newVMType == ViewModelEnum.NewGoal && passedObject is Goal g )
				(vm as IContent<Goal>)?.Fill( g );
			else if( newVMType == ViewModelEnum.NewDayTime && passedObject is DoubleTime dt )
				(vm as IContent<DoubleTime>)?.Fill( dt );
			SelectedVMEssential = vm;
		}
		#endregion

	}
}
