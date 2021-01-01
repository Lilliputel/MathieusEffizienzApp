using DataLayer;
using FrontLayer.WPF.Properties;
using FrontLayer.WPF.Windows;
using LogicLayer.Extensions;
using LogicLayer.Services;
using LogicLayer.Stores;
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
		private readonly IRepository _DataService;
		private readonly SettingsStore _Settings;
		private readonly ViewModelStore _ViewModels;
		#endregion

		#region constructor
		public App() {
#if XML
			_DataService = new XMLRepository("CategoryList", @"S:\TESTING\Effizienz\");
#elif SQLite
			_DataService = new SQLiteRepository();
#else
			_DataService = new MockRepository();
#endif
			_Settings = new SettingsStore();
			_ViewModels = new ViewModelStore( _DataService, _Settings );
		}
		#endregion

		#region OnStartup
		protected override void OnStartup( StartupEventArgs e ) {

#warning maybe the way with the eventhandler is a little sloppy

			// Add an Eventhandler to Display MessageBoxes on Alerts
			NotificationService.AlertOccured += ShowMessageBoxOnAlert;
			// Add all the EventHandlers to handle SettingChanges
			_Settings.BoolSettingChanged += BoolSettingChanged;
			_Settings.ObjectSettingChanged += ObjectSettingChanged;
			_Settings.SetCulture( CultureInfo.CreateSpecificCulture( Settings.Default.CurrentCulture ) );
			_Settings.SwitchTheme( Settings.Default.DarkMode );
			_Settings.ChangeCountDirection( Settings.Default.CountsUp );

			base.OnStartup( e );

			new MainWindow() { DataContext = new ViewModelMain( _ViewModels ) }.Show();
		}
		#endregion

		#region OnExit
		protected override void OnExit( ExitEventArgs e ) {
			Settings.Default.Save();

			_Settings.BoolSettingChanged -= BoolSettingChanged;
			_Settings.ObjectSettingChanged -= ObjectSettingChanged;

			base.OnExit( e );
		}
		#endregion

		#region private eventhandler
		private void BoolSettingChanged( BoolSettingsEnum setting, bool value ) {
			if( setting == BoolSettingsEnum.DarkMode )
				SetDarkMode( value );
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
		private void SetDarkMode( bool value ) {
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
