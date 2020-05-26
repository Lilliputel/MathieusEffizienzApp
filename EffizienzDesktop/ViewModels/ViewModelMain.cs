using Effizienz.Commands;
using Effizienz.Utility;
using System.Windows.Input;

namespace Effizienz.Views {
	public class ViewModelMain : ViewModelBase {

		private ViewModelBase selectedVMMain;
		private ViewModelBase selectedVMEssential;

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

		private ICommand _commandUpdateView;
		public ICommand CommandUpdateView => _commandUpdateView ??
			( _commandUpdateView = new CommandRelay(parameter => {
				if( parameter is string ) {
					ViewModelBase viewModel = NameContainer.GetViewModel((string)parameter, out bool isMain);
					if( isMain )
						this.SelectedVMMain = viewModel;
					else
						this.SelectedVMEssential = viewModel;
				}
			}) );

	}
}
