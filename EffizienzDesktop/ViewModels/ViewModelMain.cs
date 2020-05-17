using EffizienzNeu.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EffizienzNeu.Views {
	public class ViewModelMain : ViewModelBase {

		private ViewModelBase selectedVMMain;
		private ViewModelBase selectedVMEssential;


		public ViewModelBase SelectedVMMain {
			get { return selectedVMMain; }
			set {
				if( selectedVMMain != value ) {
					selectedVMMain = value;
					OnPropertyChanged(nameof(selectedVMMain));
				}
			}
		}
		public ViewModelBase SelectedVMEssential {
			get { return selectedVMEssential; }
			set {
				if( selectedVMEssential != value ) {
					selectedVMEssential = value;
					OnPropertyChanged(nameof(selectedVMEssential));
				}
			}
		}
		
		public ICommand CommandUpdateView { get; set; }

		public ViewModelMain() {
			this.CommandUpdateView = new CommandUpdateView(this);
		}

	}
}
