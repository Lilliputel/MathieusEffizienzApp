using Effizienz.Commands;
using Effizienz;
using System.Windows.Input;
using System.Windows;

namespace Effizienz.Views {
	public class ViewModelOptionen : ViewModelBase {

		private ICommand _commandChangeTheme;
		public ICommand CommandChangeTheme => _commandChangeTheme ??
			( _commandChangeTheme = new CommandRelay(parameter => {
				( Application.Current as App ).SwitchTheme();
			}) );
	}
}
