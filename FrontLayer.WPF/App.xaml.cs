using DataLayer;
using FrontLayer.WPF.Properties;
using FrontLayer.WPF.Windows;
using LogicLayer.Extensions;
using LogicLayer.Stores;
using LogicLayer.ViewModels;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace FrontLayer.WPF {
	public partial class App : Application {

		#region private fields
		private readonly IRepository _DataService;
		private readonly SettingsStore _Settings;
		private readonly AlertStore _AlertService;
		private readonly ViewModelStore _ViewModels;
		private static readonly string _ThemeDirectory = "/FrontLayer.WPF;component/Themes/";
		private static readonly string _ThemeDark = "ThemeDark.xaml";
		private static readonly string _ThemeLight = "ThemeLight.xaml";
		#endregion

		#region constructor
		public App() {
#if SQLite
			_DataService = new SQLiteRepository();
#else
			_DataService = new MockRepository();
#endif
			_Settings = new SettingsStore {
				CountingUpChanged = value => SetCountDirection( value ),
				CurrentCultureChanged = value => SetCurrentCulture( value ),
				DarkModeChanged = value => SetDarkMode( value ),
				PlanIntervallMinutesChanged = value => SetPlanIntervall( value ),

			};
			_AlertService = new AlertStore( ShowMessageBoxOnAlert );
			_ViewModels = new ViewModelStore( _DataService, _Settings, _AlertService );
		}
		#endregion

		#region OnStartup
		protected override void OnStartup( StartupEventArgs e ) {

			_Settings.CountingUp = Settings.Default.CountingUp;
			_Settings.CurrentCulture = CultureInfo.CreateSpecificCulture( Settings.Default.CurrentCulture );
			_Settings.DarkMode = Settings.Default.DarkMode;
			_Settings.PlanIntervall = Settings.Default.PlanIntervallFraction;

			base.OnStartup( e );
			new MainWindow() { DataContext = new ViewModelMain( _ViewModels ) }.Show();
		}
		#endregion

		#region OnExit
		protected override void OnExit( ExitEventArgs e ) {
			Settings.Default.Save();

			base.OnExit( e );
		}
		#endregion

		#region private eventhandler
		private void ShowMessageBoxOnAlert( string message, string title, AlertSymbolEnum symbol ) {
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
		private void SetCountDirection( bool value )
			=> _ViewModels?.Pomodoro.Clock.UpdateCountDirection( value is false );
		private void SetCurrentCulture( CultureInfo defaultCulture ) {
			CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
			CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
			FrameworkElement.LanguageProperty.OverrideMetadata( typeof( FrameworkElement ), new FrameworkPropertyMetadata( XmlLanguage.GetLanguage( CultureInfo.CurrentCulture.IetfLanguageTag ) ) );
			Settings.Default.CurrentCulture = defaultCulture.ToString();
		}
		private void SetDarkMode( bool value ) {
			Settings.Default.DarkMode = value;
			Resources.MergedDictionaries[0].Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add(
				new ResourceDictionary() {
					Source = new Uri( _ThemeDirectory + (value ? _ThemeDark : _ThemeLight), UriKind.Relative )
				} );
		}
		private void SetPlanIntervall( TimeSpan value ) {
			return;
		}
		#endregion

	}
}
