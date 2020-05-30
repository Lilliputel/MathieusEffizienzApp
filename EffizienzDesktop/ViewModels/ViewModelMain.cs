using System;
using Effizienz.Commands;
using Effizienz.Utility;
using System.Windows.Input;

namespace Effizienz.Views {
	public class ViewModelMain : ViewModelBase {

		#region fields

		private ViewModelBase selectedVMMain;
		private ViewModelBase selectedVMEssential;
		private ICommand _commandUpdateView;

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

		#endregion


	}
}
