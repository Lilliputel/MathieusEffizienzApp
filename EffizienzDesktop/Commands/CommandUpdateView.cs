using EffizienzNeu.Enums;
using EffizienzNeu.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EffizienzNeu.Commands {
	public class CommandUpdateView : ICommand {

		private ViewModelMain viewModelMain;
		public event EventHandler CanExecuteChanged;

		public CommandUpdateView( ViewModelMain _viewModelMain) {
			this.viewModelMain = _viewModelMain;
		}

		public bool CanExecute( object parameter ) => true;
		public void Execute( object parameter ) {
			switch( parameter ) {

			case nameof(EnumViewModels.Dashboard):
				viewModelMain.SelectedVMMain = new ViewModelDashboard();
				break;
			case nameof(EnumViewModels.Gantt):
				viewModelMain.SelectedVMMain = new ViewModelGantt();
				break;
			case nameof(EnumViewModels.Optionen):
				viewModelMain.SelectedVMMain = new ViewModelOptionen();
				break;
			case nameof(EnumViewModels.Planung):
				viewModelMain.SelectedVMMain = new ViewModelPlanung();
				break;
			case nameof(EnumViewModels.ProjektÜbersicht):
				viewModelMain.SelectedVMMain = new ViewModelProjektÜbersicht();
				break;
			case nameof(EnumViewModels.Statistik):
				viewModelMain.SelectedVMMain = new ViewModelStatistik();
				break;

			case nameof(EnumViewModels.Kategorie):
				viewModelMain.SelectedVMEssential = new ViewModelKategorie();
				break;
			case nameof(EnumViewModels.Projekt):
				viewModelMain.SelectedVMEssential = new ViewModelProjekt();
				break;
			case nameof(EnumViewModels.Aufgabe):
				viewModelMain.SelectedVMEssential = new ViewModelAufgabe();
				break;
			case nameof(EnumViewModels.Pomodoro):
				viewModelMain.SelectedVMEssential = new ViewModelPomodoro();
				break;

			default:
				break;
			}
		}
	}
}
