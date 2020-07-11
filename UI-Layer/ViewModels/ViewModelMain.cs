using System;
using System.Windows;
using System.Windows.Input;
using UiLayer.Commands;
using UiLayer.Utility;

namespace UiLayer.Views {
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
				if( parameter is string ) {
					EnumViewModels name = Enum.Parse<EnumViewModels>(parameter as string);
					ViewModelBase viewModel = NameContainer.GetViewModel(name, out bool isMain);
					if( isMain )
						this.SelectedVMMain = viewModel;
					else
						this.SelectedVMEssential = viewModel;
				}
			}) );

		public ICommand CommandCreateObjects => _commandCreateObjects ??
			( _commandCreateObjects = new CommandRelay(parameter => {
				( Application.Current as App ).GenerateObjects();
			}) );

		#endregion


	}
}
