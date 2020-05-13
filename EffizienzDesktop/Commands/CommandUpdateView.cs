using EffizienzNeu.Enums;
using EffizienzNeu.ViewModels;
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

			case nameof(EnumViewModels.Übersicht):
				viewModelMain.SelectedVMMain = new ViewModelÜbersicht();
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
				viewModelMain.SelectedVMMain = new ViewModelTEST();
				viewModelMain.SelectedVMEssential = new ViewModelTEST();
				break;
			}
		}
	}
}
