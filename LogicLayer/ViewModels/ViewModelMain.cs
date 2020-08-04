using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.Utility;
using System;
using System.Windows.Input;

namespace LogicLayer.ViewModels {
	public class ViewModelMain : ViewModelBase {

		#region fields

		private ViewModelBase selectedVMMain;
		private ViewModelBase selectedVMEssential;
		private ICommand _commandUpdateView;
		private ICommand _commandCreateObjects;

		#endregion

		#region properties

		public ViewModelBase SelectedVMMain {
			get { return selectedVMMain; }
			set {
				if( selectedVMMain != value ) {
					selectedVMMain = value;
					OnPropertyChanged(nameof(SelectedVMMain));
				}
			}
		}
		public ViewModelBase SelectedVMEssential {
			get { return selectedVMEssential; }
			set {
				if( selectedVMEssential != value ) {
					selectedVMEssential = value;
					OnPropertyChanged(nameof(SelectedVMEssential));
				}
			}
		}

		public ICommand CommandUpdateView => _commandUpdateView ??
			( _commandUpdateView = new CommandRelay(parameter => {
				if( parameter is string pString ) {
					EnumViewModels name = Enum.Parse<EnumViewModels>(pString);
					ViewModelBase viewModel = NameContainer.GetViewModel(name, out bool isMain);
					if( isMain )
						this.SelectedVMMain = viewModel;
					else
						this.SelectedVMEssential = viewModel;
				}
			}) );

		public ICommand CommandCreateObjects => _commandCreateObjects ??
			( _commandCreateObjects = new CommandRelay(parameter => {
				ObjectManager.GenerateObjects();
			}) );

		#endregion


	}
}
