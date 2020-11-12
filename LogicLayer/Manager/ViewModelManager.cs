using LogicLayer.ViewModels;
using LogicLayer.Views;
using ModelLayer.Classes;

namespace LogicLayer.Manager {

	public static class ViewModelManager {

		#region private fields
		private static readonly CategoryList CategoryList = (CategoryList) ObjectManager.CategoryList;
		private static readonly WeekPlan WeekPlan = ObjectManager.WeekPlan;

		private static DashboardViewModel _Dashboard;
		private static PlanViewModel _Plan;
		private static GoalOverviewViewModel _GoalOverview;
		private static GanttDiagramViewModel _GanttDiagram;
		private static StatisticsViewModel _Statistics;
		private static SettingsViewModel _Settings;
		private static NewCategoryViewModel _NewCategory;
		private static NewGoalViewModel _NewGoal;
		private static NewDayTimeViewModel _NewDayTime;
		#endregion

		#region public properties
		public static DashboardViewModel Dashboard => _Dashboard ??= new DashboardViewModel( CategoryList );
		public static PlanViewModel Plan => _Plan ??= new PlanViewModel( CategoryList, WeekPlan );
		public static GoalOverviewViewModel GoalOverview => _GoalOverview ??= new GoalOverviewViewModel( CategoryList );
		public static GanttDiagramViewModel GanttDiagram => _GanttDiagram ??= new GanttDiagramViewModel( CategoryList );
		public static StatisticsViewModel Statistics => _Statistics ??= new StatisticsViewModel( CategoryList );
		public static SettingsViewModel Settings => _Settings ??= new SettingsViewModel();

		public static PomodoroViewModel Pomodoro { get; }
		public static NewCategoryViewModel NewCategory => _NewCategory ??= new NewCategoryViewModel( CategoryList );
		public static NewGoalViewModel NewGoal => _NewGoal ??= new NewGoalViewModel( CategoryList );
		public static NewDayTimeViewModel NewDayTime => _NewDayTime ??= new NewDayTimeViewModel( CategoryList );
		#endregion

		#region initializer
		//TODO I should use a Method to Set the View with the Corresponding ViewModel, that accepts a passed Object
		static ViewModelManager() {
			Pomodoro = new PomodoroViewModel( new WorkItem() );
		}
		#endregion

		#region public methods
		public static ViewModelBase GetViewModel( string viewModelName )
			=> (ViewModelBase) typeof( ViewModelManager ).GetProperty( viewModelName ).GetValue( null, null );
		public static bool SetViewModel<T>( string viewModelName, T passedObject ) {
			GetViewModel( viewModelName );
			return false;
		}
		#endregion

	}

}
