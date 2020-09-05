using System;
using System.Windows.Input;

namespace LogicLayer.Commands {
	public class RelayCommand : ICommand {

		#region fields

		private readonly Action<object> _Execute;
		private readonly Func<object, bool>? _CanExecute;

		#endregion

		#region properties

		public event EventHandler CanExecuteChanged{
			add{ CommandManager.RequerySuggested += value; }
			remove{ CommandManager.RequerySuggested -= value; }
		}

		#endregion

		#region constructor

		public RelayCommand( Action<object> execute, Func<object, bool>? canExecute = null ) {
			if( execute == null )
				throw new NullReferenceException("execute");

			_Execute = execute;
			_CanExecute = canExecute;
		}

		#endregion

		#region Methods

		public bool CanExecute( object parameter ) => _CanExecute is null || _CanExecute(parameter);
		public void Execute( object parameter ) => _Execute.Invoke(parameter);

		#endregion

	}
}
