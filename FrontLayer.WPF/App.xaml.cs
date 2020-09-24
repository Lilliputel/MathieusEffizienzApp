using LogicLayer.Extensions;
using LogicLayer.Manager;
using System;
using System.Globalization;
using System.Windows;

namespace FrontLayer.WPF {
	public partial class App : Application {

		#region private fields

		private string _ThemeDirectory = "/FrontLayer.WPF;component/Themes/";
		private Uri _ThemeDarkUri;
		private Uri _ThemeLightUri;

		#endregion

		#region constructor

		public App() {
			_ThemeDarkUri = new Uri(_ThemeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute);
			_ThemeLightUri = new Uri(_ThemeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute);
		}

		#endregion

		#region OnStartup

		protected override void OnStartup( StartupEventArgs e ) {
			// Set the Default Culture
			CultureInfo.DefaultThreadCurrentCulture = CultureManager.CurrentCulture;
			CultureInfo.DefaultThreadCurrentUICulture = CultureManager.CurrentCulture;

			// Add an Eventhandler to Display MessageBoxes on Alerts
			AlertManager.AlertOccured += ShowMessageBoxOnAlert;

			// Set the Standard-Theme and add the EventHandler
			ThemeManager.DarkModeChanged += DarkModeChanged;
			ThemeManager.SwitchTheme(false);

			// Load the saved Categories from The XML-File
			ObjectManager.LoadCategories();
			//ObjectManager.LoadSettings();
			base.OnStartup(e);
		}

		#endregion

		#region private methods

		private void DarkModeChanged( bool isDarkMode ) {
			Resources.MergedDictionaries[0].Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add(
				new ResourceDictionary() {
					Source = isDarkMode ? _ThemeDarkUri : _ThemeLightUri
				});
		}

		private void ShowMessageBoxOnAlert( string message, string buttonText, string? title, AlertSymbolEnum? symbol ) {
			MessageBoxImage image = symbol switch
			{
				AlertSymbolEnum.Information => MessageBoxImage.Information,
				AlertSymbolEnum.Message => MessageBoxImage.Information,
				AlertSymbolEnum.Check => MessageBoxImage.Information,
				AlertSymbolEnum.OK => MessageBoxImage.Information,
				AlertSymbolEnum.Warning => MessageBoxImage.Warning,
				AlertSymbolEnum.Error => MessageBoxImage.Stop,
				AlertSymbolEnum.Fatal => MessageBoxImage.Stop,
				_ => MessageBoxImage.None
			};

			MessageBox.Show(message, title, MessageBoxButton.OK, image);

		}

		#endregion

	}
}
