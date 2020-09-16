using LogicLayer.ViewModels;
using LogicLayer.Views;
using ModelLayer.Classes;
using System.Collections.Generic;
using System.Linq;


namespace LogicLayer.Manager {

	public enum EnumViewModels {

		Dashboard,
		Plan,
		GoalOverview,
		GanttDiagram,
		Statistics,
		Settings,

		Pomodoro,
		NewCategory,
		NewGoal,
		NewDayTime

	}

	public static class ViewModelManager {

		#region fields

		private static Dictionary<EnumViewModels, ViewModelBase> _DictionaryViewModelsMain;
		private static Dictionary<EnumViewModels, ViewModelBase> _DictionaryViewModelsEssential;

		#endregion

		#region properties

		public static ViewModelBase? SelectedMainViewModel { get; private set; }

		public static DashboardViewModel Dashboard { get; private set; }
		public static PlanViewModel Plan { get; private set; }
		public static GoalOverviewViewModel GoalOverview { get; private set; }
		public static GanttDiagramViewModel GanttDiagram { get; private set; }
		public static StatisticsViewModel Statistics { get; private set; }
		public static SettingsViewModel Settings { get; private set; }

		public static ViewModelBase? SelectedEssentialViewModel { get; private set; }

		public static PomodoroViewModel Pomodoro { get; private set; }
		public static NewCategoryViewModel NewCategory { get; private set; }
		public static NewGoalViewModel NewGoal { get; private set; }
		public static NewDayTimeViewModel NewDayTime { get; private set; }

		#endregion

		#region initializer

		static ViewModelManager() {

			Dashboard = new DashboardViewModel();
			Plan = new PlanViewModel();
			GoalOverview = new GoalOverviewViewModel();
			GanttDiagram = new GanttDiagramViewModel();
			Statistics = new StatisticsViewModel();
			Settings = new SettingsViewModel();

			_DictionaryViewModelsMain = new Dictionary<EnumViewModels, ViewModelBase>() {
				{EnumViewModels.Dashboard, Dashboard },
				{EnumViewModels.Plan, Plan },
				{EnumViewModels.GoalOverview, GoalOverview },
				{EnumViewModels.GanttDiagram, GanttDiagram },
				{EnumViewModels.Statistics, Statistics },
				{EnumViewModels.Settings, Settings }
			};

#warning for debugging purpose a new goal gets added
			Pomodoro = new PomodoroViewModel(new Goal());
			NewCategory = new NewCategoryViewModel();
			NewGoal = new NewGoalViewModel();
			NewDayTime = new NewDayTimeViewModel();

			_DictionaryViewModelsEssential = new Dictionary<EnumViewModels, ViewModelBase>(){
				{EnumViewModels.Pomodoro, Pomodoro },
				{EnumViewModels.NewCategory, NewCategory },
				{EnumViewModels.NewGoal, NewGoal },
				{EnumViewModels.NewDayTime, NewDayTime }
			};

		}

		#endregion

		#region methods

		public static ViewModelBase? GetViewModel( EnumViewModels viewModelName ) =>
				_DictionaryViewModelsMain.GetValueOrDefault(viewModelName) ??
				_DictionaryViewModelsEssential.GetValueOrDefault(viewModelName);
		public static ViewModelBase? GetViewModel( EnumViewModels viewModelName, out bool isMain ) {
			ViewModelBase? returnedViewModel;
			if( _DictionaryViewModelsMain.TryGetValue(viewModelName, out returnedViewModel) ) {
				isMain = true;
			}
			else {
				_DictionaryViewModelsEssential.TryGetValue(viewModelName, out returnedViewModel);
				isMain = false;
			}
			return returnedViewModel;
		}

		public static void SetViewModel( EnumViewModels viewModelName ) {
			ViewModelBase? viewModel = GetViewModel(viewModelName, out bool isMain);
			if( isMain is true )
				SelectedMainViewModel = viewModel;
			else
				SelectedEssentialViewModel = viewModel;
		}

		public static string GetName( ViewModelBase viewModel ) =>
				_DictionaryViewModelsMain.FirstOrDefault(x => x.Value == viewModel).Key.ToString() ??
				_DictionaryViewModelsEssential.FirstOrDefault(x => x.Value == viewModel).Key.ToString();

		#endregion

	}
}
