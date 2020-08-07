using LogicLayer.ViewModels;

namespace LogicLayer.Views {
	public class SettingsViewModel : ViewModelBase {

#warning have to implement a way to control the slected ViewModel without touching the FrontLayer

		//private ICommand _commandChangeTheme;
		//public ICommand CommandChangeTheme => _commandChangeTheme ??
		//	( _commandChangeTheme = new RelayCommand(parameter => {
		//		( Application.Current as App ).SwitchTheme();
		//		this.ThemeButton = ( Application.Current as App ).GetDarkMode() ? "Light!" : "Dark!";
		//	}) );

		//private string themeButton;
		//public string ThemeButton {
		//	get {
		//		return themeButton;
		//	}
		//	set {
		//		themeButton = value;
		//		OnPropertyChanged(nameof(ThemeButton));
		//	}
		//}

		//public SettingsViewModel() {
		//	this.ThemeButton = ( Application.Current as App ).GetDarkMode() ? "Light!" : "Dark!";
		//}
	}
}
