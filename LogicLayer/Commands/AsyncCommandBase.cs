using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogicLayer.Commands {
	public abstract class AsyncCommandBase : ICommand {

		#region fields

		private bool _isExecuting;
		protected abstract Task ExecuteAsync( object parameter );

		private readonly Action<Exception> _onException;

		#endregion

		#region properties

		public bool IsExecuting {
			get {
				return _isExecuting;
			}
			set {
				_isExecuting = value;
				CanExecuteChanged?.Invoke(this, new EventArgs());
			}
		}
		public event EventHandler? CanExecuteChanged;

		#endregion

		#region constructors

		public AsyncCommandBase( Action<Exception> onException ) => _onException = onException;

		#endregion

		#region methods

		public bool CanExecute( object parameter ) => !IsExecuting;
		public async void Execute( object parameter ) {
			IsExecuting = true;
			try {
				await ExecuteAsync(parameter);
			}
			catch( Exception ex ) {
				_onException?.Invoke(ex);
			}
			IsExecuting = false;
		}

		#endregion

	}
}
