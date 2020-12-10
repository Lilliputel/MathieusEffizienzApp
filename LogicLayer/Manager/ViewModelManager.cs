using DataLayer;
using LogicLayer.ViewModels;
using LogicLayer.Views;
using ModelLayer.Enums;
using System;

namespace LogicLayer.Manager {

	public static class ViewModelManager {

		#region private fields
		private static readonly IRepository DataService = ObjectManager.DataService;

		private static DashboardViewModel? _Dashboard;
		private static PlanViewModel? _Plan;
		private static GoalOverviewViewModel? _GoalOverview;
		private static GanttDiagramViewModel? _GanttDiagram;
		private static StatisticsViewModel? _Statistics;
		private static SettingsViewModel? _Settings;
		private static NewCategoryViewModel? _NewCategory;
		private static NewGoalViewModel? _NewGoal;
		private static NewDayTimeViewModel? _NewDayTime;
		private static PomodoroViewModel? _Pomodoro;
		#endregion

		#region public properties
		public static DashboardViewModel Dashboard
			=> _Dashboard ??= new DashboardViewModel( DataService );
		public static PlanViewModel Plan
			=> _Plan ??= new PlanViewModel( DataService );
		public static GoalOverviewViewModel Overview
			=> _GoalOverview ??= new GoalOverviewViewModel( DataService );
		public static GanttDiagramViewModel Gantt
			=> _GanttDiagram ??= new GanttDiagramViewModel( DataService );
		public static StatisticsViewModel Statistics
			=> _Statistics ??= new StatisticsViewModel( DataService );
		public static SettingsViewModel Settings
			=> _Settings ??= new SettingsViewModel();
		public static PomodoroViewModel Pomodoro
			=> _Pomodoro ??= new PomodoroViewModel();
		public static NewCategoryViewModel NewCategory
			=> _NewCategory ??= new NewCategoryViewModel( DataService );
		public static NewGoalViewModel NewGoal
			=> _NewGoal ??= new NewGoalViewModel( DataService );
		public static NewDayTimeViewModel NewTime
			=> _NewDayTime ??= new NewDayTimeViewModel( DataService );
		#endregion

		#region public methods
		public static ViewModelBase? GetViewModel( ViewModelEnum viewModel )
			=> typeof( ViewModelManager )
			.GetProperty( Enum.GetName( typeof( ViewModelEnum ), viewModel ) )?
			.GetValue( null, null ) as ViewModelBase;
		public static bool SetViewModel<T>( ViewModelEnum viewModel, T passedObject ) {
			GetViewModel( viewModel );
			return false;
		}
		#endregion

	}

}
