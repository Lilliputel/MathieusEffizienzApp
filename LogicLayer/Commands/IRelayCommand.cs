using System;
using System.Windows.Input;

namespace LogicLayer.Commands {
	public interface IRelayCommand : ICommand {
		public void RaiseCanExecuteChanged( object? sender, EventArgs? e = null );
	}
}
