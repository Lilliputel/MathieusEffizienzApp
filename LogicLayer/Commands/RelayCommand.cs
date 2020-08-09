﻿using System;
using System.Windows.Input;

namespace LogicLayer.Commands {
	public class RelayCommand : ICommand {

		#region fields

		private readonly Action<object> _execute;
		private readonly Func<object, bool>? _canExecute;

		#endregion

		#region properties

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		#endregion

		#region constructor

		public RelayCommand( Action<object> execute, Func<object, bool>? canExecute = null ) {
			_execute = execute;
			_canExecute = canExecute;
		}

		#endregion

		#region Methods

		public bool CanExecute( object parameter ) => _canExecute is null || _canExecute(parameter);
		public void Execute( object parameter ) => _execute(parameter);

		#endregion


	}
}
