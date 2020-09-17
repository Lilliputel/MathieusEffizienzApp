using LogicLayer.Extensions;
using LogicLayer.ViewModels;
using LogicLayer.Views;
using ModelLayer.Classes;


namespace LogicLayer.Manager {

	public static class ViewModelManager {

		#region properties

		public static DashboardViewModel Dashboard { get; private set; }
		public static PlanViewModel Plan { get; private set; }
		public static GoalOverviewViewModel GoalOverview { get; private set; }
		public static GanttDiagramViewModel GanttDiagram { get; private set; }
		public static StatisticsViewModel Statistics { get; private set; }
		public static SettingsViewModel Settings { get; private set; }

		public static PomodoroViewModel Pomodoro { get; private set; }
		public static NewCategoryViewModel NewCategory { get; private set; }
		public static NewGoalViewModel NewGoal { get; private set; }
		public static NewDayTimeViewModel NewDayTime { get; private set; }

		#endregion

		#region public events

		public static event ViewModelChangedEventHandler? MainViewModelChanged;
		public static event ViewModelChangedEventHandler? EssentialViewModelChanged;

		#endregion

		#region initializer

		static ViewModelManager() {

			Dashboard = new DashboardViewModel();
			Plan = new PlanViewModel();
			GoalOverview = new GoalOverviewViewModel();
			GanttDiagram = new GanttDiagramViewModel();
			Statistics = new StatisticsViewModel();
			Settings = new SettingsViewModel();

#warning for debugging purpose a new goal gets added
			Pomodoro = new PomodoroViewModel(new Goal());
			NewCategory = new NewCategoryViewModel();
			NewGoal = new NewGoalViewModel();
			NewDayTime = new NewDayTimeViewModel();

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
