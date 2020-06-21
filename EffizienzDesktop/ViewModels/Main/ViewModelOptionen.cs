using Effizienz.Commands;
using System.Windows;
using System.Windows.Input;

namespace Effizienz.Views {
	public class ViewModelOptionen : ViewModelBase {

		private ICommand _commandChangeTheme;
		public ICommand CommandChangeTheme => _commandChangeTheme ??
			( _commandChangeTheme = new CommandRelay(parameter => {
				( Application.Current as App ).SwitchTheme();
				this.ThemeButton = ( Application.Current as App ).GetDarkMode() ? "Light!" : "Dark!";
			}) );

		private string themeButton;
		public string ThemeButton {
			get {
				return themeButton;
			}
			set {
				themeButton = value;
				OnPropertyChanged(nameof(ThemeButton));
			}
		}

		public ViewModelOptionen() {
			this.ThemeButton = (Application.Current as App).GetDarkMode() ? "Light!" : "Dark!";
		}
	}
}
