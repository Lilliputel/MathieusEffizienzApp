using LogicLayer.Commands;
using LogicLayer.Manager;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace LogicLayer.ViewModels {
	public class ViewModelMain : ViewModelBase {

		#region fields

		private ICommand? _commandUpdateView;
		private ICommand? _commandCreateObjects;

		#endregion

		#region properties

		public ViewModelBase? SelectedVMMain { get; private set; }
		public ViewModelBase? SelectedVMEssential { get; private set; }

		public ICommand CommandUpdateView => _commandUpdateView ??=
			new RelayCommand(parameter => {
				if( parameter is string pString ) {

					if( Enum.TryParse(pString, out EnumMainViewModels mainName) == true )
						UpdateSelectedMainViewModel(ViewModelManager.GetMainViewModel(mainName), null);
					else if( Enum.TryParse(pString, out EnumEssentialViewModels essentialName) == true )
						UpdateSelectedEssentialViewModel(ViewModelManager.GetEssentialViewModel(essentialName), null);
					else
						Debug.WriteLine($"Could not Parse the string {pString} to an EnumMainViewModels or a EnumEssentialViewModels!");
				}
			});

		public ICommand CommandCreateObjects => _commandCreateObjects ??=
			new RelayCommand(parameter => {
				Debug.WriteLine("Tried to Generate Objects in the MainViewModel!");
			});

		#endregion

		#region constructor

		public ViewModelMain() {
			ViewModelManager.MainViewModelChanged += UpdateSelectedMainViewModel;
			ViewModelManager.EssentialViewModelChanged += UpdateSelectedEssentialViewModel;
		}

		#endregion

		#region public methods

		private void UpdateSelectedMainViewModel( ViewModelBase newViewModel, object? passedObject ) {
			this.SelectedVMMain = newViewModel;
		}
		private void UpdateSelectedEssentialViewModel( ViewModelBase newViewModel, object? passedObject ) {
			this.SelectedVMEssential = newViewModel;
		}

		#endregion
	}
}
