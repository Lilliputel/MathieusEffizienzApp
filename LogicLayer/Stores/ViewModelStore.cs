using DataLayer;
using LogicLayer.BaseViewModels;
using LogicLayer.Views;
using ModelLayer.Enums;
using System;

namespace LogicLayer.Stores {
	public class ViewModelStore {

		#region private fields
		private readonly IRepository _DataService;
		private readonly SettingsStore _SettingsStore;

		private DashboardViewModel? _Dashboard;
		private PlanViewModel? _Plan;
		private GoalOverviewViewModel? _GoalOverview;
		private GanttDiagramViewModel? _GanttDiagram;
		private StatisticsViewModel? _Statistics;
		private SettingsViewModel? _Settings;
		private NewCategoryViewModel? _NewCategory;
		private NewGoalViewModel? _NewGoal;
		private NewDayTimeViewModel? _NewDayTime;
		private PomodoroViewModel? _Pomodoro;
		#endregion

		#region public properties
		public DashboardViewModel Dashboard
			=> _Dashboard ??= new DashboardViewModel( _DataService );
		public PlanViewModel Plan
			=> _Plan ??= new PlanViewModel( _DataService );
		public GoalOverviewViewModel GoalOverview
			=> _GoalOverview ??= new GoalOverviewViewModel( _DataService );
		public GanttDiagramViewModel GanttDiagram
			=> _GanttDiagram ??= new GanttDiagramViewModel( _DataService );
		public StatisticsViewModel Statistics
			=> _Statistics ??= new StatisticsViewModel( _DataService );
		public SettingsViewModel Settings
			=> _Settings ??= new SettingsViewModel( this, _SettingsStore );
		public PomodoroViewModel Pomodoro
			=> _Pomodoro ??= new PomodoroViewModel();
		public NewCategoryViewModel NewCategory
			=> _NewCategory ??= new NewCategoryViewModel( _DataService );
		public NewGoalViewModel NewGoal
			=> _NewGoal ??= new NewGoalViewModel( _DataService );
		public NewDayTimeViewModel NewDayTime
			=> _NewDayTime ??= new NewDayTimeViewModel( _DataService );
		#endregion

		#region constructor
		public ViewModelStore( IRepository dataService, SettingsStore settingsStore ) {
			_DataService = dataService;
			_SettingsStore = settingsStore;
		}
		#endregion

		#region public methods
		public ViewModelBase? GetViewModel( ViewModelEnum viewModel ) {
			var name = Enum.GetName( typeof( ViewModelEnum ), viewModel );
			if( string.IsNullOrWhiteSpace( name ) )
				throw new ArgumentException( $"{nameof( GetViewModel )} must get a valid {nameof( ViewModelEnum )}" );
			return typeof( ViewModelStore ).GetProperty( name )?.GetValue( this, null ) as ViewModelBase;
		}
		#endregion

	}
}
