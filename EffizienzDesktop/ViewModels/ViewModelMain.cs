using EffizienzNeu.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EffizienzNeu.ViewModels {
	public class ViewModelMain : ViewModelBase {

		private ViewModelBase selectedViewModel;

		public ViewModelBase SelectedViewModel {
			get { return selectedViewModel; }
			set {
				if(selectedViewModel != value) {
					selectedViewModel = value;
					OnPropertyChanged(nameof(SelectedViewModel));
				}
			}
		}

		public ICommand CommandUpdateView { get; set; }

		public ViewModelMain() {
			this.CommandUpdateView = new CommandUpdateView(this);

		}
	}
}
