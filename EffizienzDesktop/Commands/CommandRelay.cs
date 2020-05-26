using System;
using System.Windows.Input;

namespace Effizienz.Commands {
	public class CommandRelay : ICommand {

		private readonly Action<object> _execute;
		private readonly Func<object, bool> _canExecute;

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		public CommandRelay( Action<object> execute, Func<object, bool> canExecute = null ) {
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute( object parameter ) => _canExecute == null || _canExecute(parameter);
		public void Execute( object parameter ) => _execute(parameter);
	}
}
