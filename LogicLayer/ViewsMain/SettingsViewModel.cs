using LogicLayer.BaseViewModels;
using LogicLayer.Commands;
using LogicLayer.Stores;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class SettingsViewModel : ViewModelBase {

		#region private fields
		private readonly SettingsStore _SettingsStore;
		private ICommand? _CommandChangeTheme;
		private ICommand? _CommandChangeCountDirection;
		private ICommand? _CommandChangePlanIntervall;
		#endregion

		#region public properties
		public int PlanIntervallMinutes { get; set; }
		public string ThemeButton
			=> _SettingsStore.DarkMode ? "Light!" : "Dark!";
		public CultureInfo SelectedCulture {
			get => _SettingsStore.CurrentCulture;
			set {
				if( value != _SettingsStore.CurrentCulture ) {
					_SettingsStore.CurrentCulture = value;
					Trace.WriteLine( $"SettingsVM set the Culture to {_SettingsStore.CurrentCulture}!" );
				}
			}
		}
		public CultureInfo[] Cultures { get; }
		#endregion

		#region public commands
		public ICommand CommandChangeTheme => _CommandChangeTheme ??=
			new RelayCommand( parameter => {
				_SettingsStore.SwitchDarkMode();
				RaisePropertyChanged( nameof( ThemeButton ) );
				Trace.WriteLine( $"SettingsVM set the Darkmode to {_SettingsStore.DarkMode}!" );
			} );
		public ICommand CommandChangeCountDirection => _CommandChangeCountDirection ??=
			new RelayCommand( parameter => {
				_SettingsStore.SwitchCountingUp();
				Trace.WriteLine( $"SettingsVM set the CountDirection to {_SettingsStore.CountingUp}!" );
			} );
		public ICommand CommandChangePlanIntervall => _CommandChangePlanIntervall
			??= new RelayCommand( parameter => {
				_SettingsStore.PlanIntervallMinutes = PlanIntervallMinutes;
				Trace.WriteLine( $"SettingsVM set the PlanIntervall to {PlanIntervallMinutes}min.!" );
			} );
		#endregion

		#region constructor
		public SettingsViewModel( SettingsStore settingsStore ) {
			_SettingsStore = settingsStore;
			PlanIntervallMinutes = _SettingsStore.PlanIntervallMinutes;
			Cultures = CultureInfo.GetCultures( CultureTypes.SpecificCultures );
		}
		#endregion

	}
}
