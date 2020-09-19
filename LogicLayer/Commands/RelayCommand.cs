using System;
using System.Windows.Input;

namespace LogicLayer.Commands {

	public class RelayCommand : ICommand {

		#region fields

		private readonly Action<object> _Execute;
		private readonly Predicate<object>? _CanExecute;

		#endregion

		#region public events

#warning https://stackoverflow.com/questions/34996198/the-name-commandmanager-does-not-exist-in-the-current-context-visual-studio-2

		public event EventHandler? CanExecuteChanged;

		#endregion

		#region constructor

		public RelayCommand( Action<object> execute, Predicate<object>? canExecute = null ) {
			if( execute is null )
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
