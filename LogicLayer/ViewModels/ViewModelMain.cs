using LogicLayer.Commands;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace LogicLayer.ViewModels {
	public class ViewModelMain : ViewModelBase {

		#region private fields
		private ICommand? _commandUpdateView;
		private ICommand? _commandCreateObjects;
		#endregion

		#region public properties
		public ViewModelEnum? SelectedVMMain { get; private set; }
		public ViewModelEnum? SelectedVMEssential { get; private set; }
		public ICommand CommandUpdateView => _commandUpdateView ??=
			new RelayCommand( parameter => {
				if( parameter is string pString ) {
					ViewModelEnum viewModel = Enum.Parse<ViewModelEnum>( pString.Substring( 1 ) );
					if( pString[0] == 'M' )
						UpdateSelectedMainViewModel( viewModel, null );
					else if( pString[0] == 'E' )
						UpdateSelectedEssentialViewModel( viewModel, null );
					else
						Debug.WriteLine( $"Could not Parse the string {pString} to a ViewModel!" );
				}
			} );
		public ICommand CommandCreateObjects => _commandCreateObjects ??=
			new RelayCommand( parameter => {
				Debug.WriteLine( "Tried to Generate Objects in the MainViewModel!" );
			} );
		#endregion

		#region constructor
		public ViewModelMain() { }
		#endregion

		#region public methods
		private void UpdateSelectedMainViewModel( ViewModelEnum newViewModel, object? passedObject ) {
			Debug.WriteLine( $"set the main VM to {newViewModel}" );
			SelectedVMMain = newViewModel;
		}
		private void UpdateSelectedEssentialViewModel( ViewModelEnum newViewModel, object? passedObject ) {
			Debug.WriteLine( $"set the essential VM to {newViewModel}" );
			SelectedVMEssential = newViewModel;
		}
		#endregion
	}
}
