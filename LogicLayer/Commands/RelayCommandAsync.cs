using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogicLayer.Commands {
	public class RelayCommandAsync : ICommand {

		#region fields

		private bool _IsExecuting;

		private readonly Func<Task> _Execute;
		private readonly Predicate<object>? _CanExecute;
		private readonly Action<Exception>? _OnException;

		#endregion

		#region properties

		public bool IsExecuting {
			get { return _IsExecuting; }
			set {
				_IsExecuting = value;
				CanExecuteChanged?.Invoke(this, new EventArgs());
			}
		}
		public event EventHandler? CanExecuteChanged;

		#endregion

		#region constructors

		public RelayCommandAsync( Func<Task> execute, Predicate<object>? canExecute = null, Action<Exception>? onException = null ) {
			if( execute is null )
				throw new NullReferenceException("execute");

			this._Execute = execute;
			this._CanExecute = canExecute;
			this._OnException = onException;
		}

		#endregion

		#region methods

		public async Task ExecuteAsync( object parameter )
			=> await this._Execute();

		public bool CanExecute( object parameter )
			=> IsExecuting is false || _CanExecute is null || _CanExecute(parameter);

		public async void Execute( object parameter ) {
			IsExecuting = true;
			try {
				await this.ExecuteAsync(parameter);
			}
			catch( Exception ex ) {
				_OnException?.Invoke(ex);
			}
			IsExecuting = false;
		}

		#endregion

	}
}
