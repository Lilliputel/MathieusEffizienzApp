using EffizienzNeu.Enums;
using EffizienzNeu.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EffizienzNeu.Commands {
	public class CommandUpdateView : ICommand {

		private ViewModelMain viewModelMain;
		public event EventHandler CanExecuteChanged;

		public CommandUpdateView(ViewModelMain _viewModelMain) {
			this.viewModelMain = _viewModelMain;
		}

		public bool CanExecute( object parameter ) => true;
		public void Execute( object parameter ) {
			switch( parameter ) {
			case nameof(EnumViewModels.Übersicht):
				viewModelMain.SelectedViewModel = new ViewModelÜbersicht();
				break;
			case nameof(EnumViewModels.Kategorie):
				viewModelMain.SelectedViewModel = new ViewModelKategorie();
				break;
			case nameof(EnumViewModels.Projekt):
				viewModelMain.SelectedViewModel = new ViewModelProjekt();
				break;
			case nameof(EnumViewModels.Aufgabe):
				viewModelMain.SelectedViewModel = new ViewModelAufgabe();
				break;
			case nameof(EnumViewModels.Pomodoro):
				viewModelMain.SelectedViewModel = new ViewModelPomodoro();
				break;
			case nameof(EnumViewModels.Statistik):
				viewModelMain.SelectedViewModel = new ViewModelStatistik();
				break;
			default:
				break;
			}
		}
	}
}
