using LogicLayer.Extensions;
using LogicLayer.ViewModels;
using LogicLayer.Views;
using ModelLayer.Classes;
using System.Collections.Generic;


namespace LogicLayer.Manager {

	public enum EnumMainViewModels {

		Dashboard,
		Plan,
		GoalOverview,
		GanttDiagram,
		Statistics,
		Settings,

	}
	public enum EnumEssentialViewModels {

		Pomodoro,
		NewCategory,
		NewGoal,
		NewDayTime

	}

	public static class ViewModelManager {

		#region fields

		private static Dictionary<EnumMainViewModels, ViewModelBase> _DictionaryMainViewModels;
		private static Dictionary<EnumEssentialViewModels, ViewModelBase> _DictionaryEssentialViewModels;

		#endregion

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

			_DictionaryMainViewModels = new Dictionary<EnumMainViewModels, ViewModelBase>() {
				{EnumMainViewModels.Dashboard, Dashboard },
				{EnumMainViewModels.Plan, Plan },
				{EnumMainViewModels.GoalOverview, GoalOverview },
				{EnumMainViewModels.GanttDiagram, GanttDiagram },
				{EnumMainViewModels.Statistics, Statistics },
				{EnumMainViewModels.Settings, Settings }
			};

#warning for debugging purpose a new goal gets added
			Pomodoro = new PomodoroViewModel(new Goal());
			NewCategory = new NewCategoryViewModel();
			NewGoal = new NewGoalViewModel();
			NewDayTime = new NewDayTimeViewModel();

			_DictionaryEssentialViewModels = new Dictionary<EnumEssentialViewModels, ViewModelBase>(){
				{EnumEssentialViewModels.Pomodoro, Pomodoro },
				{EnumEssentialViewModels.NewCategory, NewCategory },
				{EnumEssentialViewModels.NewGoal, NewGoal },
				{EnumEssentialViewModels.NewDayTime, NewDayTime }
			};

		}

		#endregion

		#region public methods

		public static ViewModelBase GetMainViewModel( EnumMainViewModels viewModelName )
			=> _DictionaryMainViewModels[viewModelName];
		public static ViewModelBase GetEssentialViewModel( EnumEssentialViewModels viewModelName )
			=> _DictionaryEssentialViewModels[viewModelName];

		public static void OnMainViewModelChanged( ViewModelBase newViewModel, object? passedObject ) {
			MainViewModelChanged?.Invoke(newViewModel, passedObject);
		}
		public static void OnEssentialViewModelChanged( ViewModelBase newViewModel, object? passedObject ) {
			EssentialViewModelChanged?.Invoke(newViewModel, passedObject);
		}

		#endregion

	}

}
