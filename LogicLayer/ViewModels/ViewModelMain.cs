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

		public ViewModelBase? SelectedVMMain
			=> ViewModelManager.SelectedMainViewModel;
		public ViewModelBase? SelectedVMEssential
			=> ViewModelManager.SelectedEssentialViewModel;

		public ICommand CommandUpdateView => _commandUpdateView ??=
			new RelayCommand(parameter => {
				if( parameter is string pString ) {
					EnumViewModels name = Enum.Parse<EnumViewModels>(pString);
					ViewModelManager.SetViewModel(name);
					OnPropertyChanged(nameof(SelectedVMEssential));
					OnPropertyChanged(nameof(SelectedVMMain));
				}
			});

		public ICommand CommandCreateObjects => _commandCreateObjects ??=
			new RelayCommand(parameter => {
				Debug.WriteLine("Tried to Generate Objects in the MainViewModel!");
			});

		#endregion


	}
}
