using FrontLayer.WPF.Properties;
using FrontLayer.WPF.Windows;
using LogicLayer.Extensions;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using System;
using System.Globalization;
using System.Windows;

namespace FrontLayer.WPF {
	public partial class App : Application {

		#region private fields
		private static string _ThemeDirectory = "/FrontLayer.WPF;component/Themes/";
		private Uri _ThemeDarkUri = new Uri( _ThemeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute );
		private Uri _ThemeLightUri = new Uri( _ThemeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute );
		#endregion

		#region constructor
		public App() { }
		#endregion

		#region OnStartup
		protected override void OnStartup( StartupEventArgs e ) {

			// Add an Eventhandler to Display MessageBoxes on Alerts
			AlertManager.AlertOccured += ShowMessageBoxOnAlert;
			// Add all the EventHandlers to handle SettingChanges
			SettingsManager.BoolSettingChanged += BoolSettingChanged;
			SettingsManager.ObjectSettingChanged += ObjectSettingChanged;

			SettingsManager.SetCulture( CultureInfo.CreateSpecificCulture( Settings.Default.CurrentCulture ) );
			SettingsManager.SwitchTheme( Settings.Default.DarkMode );
			SettingsManager.ChangeCountDirection( Settings.Default.CountsUp );

			ObjectManager.LoadCategories();

			base.OnStartup( e );

			new MainWindow() { DataContext = new ViewModelMain() }.Show();
		}
		#endregion

		#region OnExit
		protected override void OnExit( ExitEventArgs e ) {
			Settings.Default.Save();

			SettingsManager.BoolSettingChanged -= BoolSettingChanged;
			SettingsManager.ObjectSettingChanged -= ObjectSettingChanged;

			base.OnExit( e );
		}
		#endregion

		#region private eventhandler
		private void BoolSettingChanged( BoolSettingsEnum setting, bool value ) {
			if( setting == BoolSettingsEnum.DarkMode )
				setDarkMode( value );
		}
		private void ObjectSettingChanged( ObjectSettingsEnum setting, object value ) {
			if( setting == ObjectSettingsEnum.Culture )
				SetDefaultCulture( (CultureInfo)value );
		}
		private void ShowMessageBoxOnAlert( string message, string buttonText, string? title, AlertSymbolEnum? symbol ) {
			MessageBoxImage image = symbol switch {
				AlertSymbolEnum.Information => MessageBoxImage.Information,
				AlertSymbolEnum.Message => MessageBoxImage.Information,
				AlertSymbolEnum.Check => MessageBoxImage.Information,
				AlertSymbolEnum.OK => MessageBoxImage.Information,
				AlertSymbolEnum.Warning => MessageBoxImage.Warning,
				AlertSymbolEnum.Error => MessageBoxImage.Stop,
				AlertSymbolEnum.Fatal => MessageBoxImage.Stop,
				_ => MessageBoxImage.None
			};
			MessageBox.Show( message, title, MessageBoxButton.OK, image );
		}
		#endregion

		#region private helper methods
		private void setDarkMode( bool value ) {
			Settings.Default.DarkMode = value;
			Resources.MergedDictionaries[0].Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add(
				new ResourceDictionary() {
					Source = value ? _ThemeDarkUri : _ThemeLightUri
				} );
		}
		private void SetDefaultCulture( CultureInfo defaultCulture ) {
			CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
			CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
			Settings.Default.CurrentCulture = defaultCulture.ToString();
		}
		#endregion

	}
}
