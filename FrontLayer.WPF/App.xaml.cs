using FrontLayer.WPF.Properties;
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
			_ThemeDarkUri = new Uri( _ThemeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute );
			_ThemeLightUri = new Uri( _ThemeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute );
		}
		#endregion

		#region OnStartup
		protected override void OnStartup( StartupEventArgs e ) {

			// Add an Eventhandler to Display MessageBoxes on Alerts
			AlertManager.AlertOccured += ShowMessageBoxOnAlert;
			// Add all the EventHandlers to handle SettingChanges
			SettingsManager.BoolSettingChanged += BoolSettingChanged;
			SettingsManager.ObjectSettingChanged += ObjectSettingChanged;

			// Set the default culture
			SettingsManager.SetCulture( CultureInfo.CreateSpecificCulture( Settings.Default.CurrentCulture ) );
			// Set the default theme
			SettingsManager.SwitchTheme( Settings.Default.DarkMode );
			// Set the default Countdirection
			SettingsManager.ChangeCountDirection( Settings.Default.CountsUp );

			// Load the saved Categories from The XML-File
			ObjectManager.LoadCategories();

			//Standard procedure
			base.OnStartup( e );
		}
		#endregion

		#region OnExit
		protected override void OnExit( ExitEventArgs e ) {
			//Save the settings
			Settings.Default.Save();

			// remove all the EventHandlers from the SettingsChanges
			SettingsManager.BoolSettingChanged -= BoolSettingChanged;
			SettingsManager.ObjectSettingChanged -= ObjectSettingChanged;

			//Standard procedure
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
				SetDefaultCulture( (CultureInfo) value );
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
