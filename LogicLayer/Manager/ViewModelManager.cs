using LogicLayer.ViewModels;
using LogicLayer.Views;
using ModelLayer.Classes;

namespace LogicLayer.Manager {

	public static class ViewModelManager {

		#region private fields
		private static readonly CategoryList CategoryList = (CategoryList) ObjectManager.CategoryList;
		private static readonly WeekPlan WeekPlan = ObjectManager.WeekPlan;
		#endregion

		#region public properties
		public static DashboardViewModel Dashboard { get; } = new DashboardViewModel( CategoryList );
		public static PlanViewModel Plan { get; } = new PlanViewModel( CategoryList, WeekPlan );
		public static GoalOverviewViewModel GoalOverview { get; } = new GoalOverviewViewModel( CategoryList );
		public static GanttDiagramViewModel GanttDiagram { get; } = new GanttDiagramViewModel( CategoryList );
		public static StatisticsViewModel Statistics { get; } = new StatisticsViewModel( CategoryList );
		public static SettingsViewModel Settings { get; } = new SettingsViewModel();

		public static PomodoroViewModel Pomodoro { get; }
		public static NewCategoryViewModel NewCategory { get; } = new NewCategoryViewModel( CategoryList );
		public static NewGoalViewModel NewGoal { get; } = new NewGoalViewModel( CategoryList );
		public static NewDayTimeViewModel NewDayTime { get; } = new NewDayTimeViewModel( CategoryList );
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
