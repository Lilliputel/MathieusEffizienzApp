using System;
using System.Threading.Tasks;

namespace LogicLayer.Commands {
	public class AsyncRelayCommand : AsyncCommandBase {

		#region fields

		private readonly Func<Task> _execute;

		#endregion

		#region constructors

		public AsyncRelayCommand( Func<Task> execute, Action<Exception> onException ) : base(onException) => _execute = execute;

		#endregion

		#region methods

		protected override async Task ExecuteAsync( object parameter ) {
			await _execute();
		}

		#endregion

	}
}
