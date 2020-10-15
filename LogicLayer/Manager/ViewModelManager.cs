using LogicLayer.Extensions;
using LogicLayer.ViewModels;
using LogicLayer.Views;
using ModelLayer.Classes;

namespace LogicLayer.Manager {

	public static class ViewModelManager {

		#region private fields
		private static CategoryList CategoryList = (CategoryList)ObjectManager.CategoryList;
		private static WeekPlan WeekPlan = ObjectManager.WeekPlan;
		#endregion

		#region public properties
		public static DashboardViewModel Dashboard { get; private set; } = new DashboardViewModel(CategoryList);
		public static PlanViewModel Plan { get; private set; } = new PlanViewModel(CategoryList, WeekPlan);
		public static GoalOverviewViewModel GoalOverview { get; private set; } = new GoalOverviewViewModel(CategoryList);
		public static GanttDiagramViewModel GanttDiagram { get; private set; } = new GanttDiagramViewModel(CategoryList);
		public static StatisticsViewModel Statistics { get; private set; } = new StatisticsViewModel(CategoryList);
		public static SettingsViewModel Settings { get; private set; } = new SettingsViewModel();

		public static PomodoroViewModel Pomodoro { get; private set; }
		public static NewCategoryViewModel NewCategory { get; private set; } = new NewCategoryViewModel(CategoryList);
		public static NewGoalViewModel NewGoal { get; private set; } = new NewGoalViewModel(CategoryList);
		public static NewDayTimeViewModel NewDayTime { get; private set; } = new NewDayTimeViewModel(CategoryList);

		#endregion

		#region public events

		public static event ViewModelChangedEventHandler? MainViewModelChanged;
		public static event ViewModelChangedEventHandler? EssentialViewModelChanged;

		#endregion

		#region initializer

		static ViewModelManager() {
#warning for debugging purpose a new goal gets added
			Pomodoro = new PomodoroViewModel(new WorkItem());
		}

		#endregion

		#region public methods

		public static ViewModelBase GetViewModel( string viewModelName )
			=> (ViewModelBase)typeof(ViewModelManager).GetProperty(viewModelName).GetValue(null, null);

		public static void OnMainViewModelChanged( ViewModelBase newViewModel, object? passedObject ) {
			MainViewModelChanged?.Invoke(newViewModel, passedObject);
		}
		public static void OnEssentialViewModelChanged( ViewModelBase newViewModel, object? passedObject ) {
			EssentialViewModelChanged?.Invoke(newViewModel, passedObject);
		}

		#endregion

	}

}
