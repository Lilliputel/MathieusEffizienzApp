using System;
using System.Windows.Input;

namespace LogicLayer.Commands {

	public class RelayCommand : ICommand {

		#region private fields
		private readonly Action<object> _Execute;
		private readonly Predicate<object>? _CanExecute;
		#endregion

		#region public events
		public event EventHandler? CanExecuteChanged;
		public void RaiseCanExecuteChanged( object sender, EventArgs? e = null )
			=> CanExecuteChanged?.Invoke( sender, e ?? EventArgs.Empty );
		#endregion

		#region constructor
		public RelayCommand( Action<object> execute, Predicate<object>? canExecute = null ) {
			if( execute is null )
				throw new NullReferenceException( "Execute has to be set!" );
			_Execute = execute;
			_CanExecute = canExecute;
		}
		#endregion

		#region public Methods
		public bool CanExecute( object parameter )
			=> _CanExecute is null || _CanExecute( parameter );
		public void Execute( object parameter )
			=> _Execute.Invoke( parameter );
		#endregion

	}
}
