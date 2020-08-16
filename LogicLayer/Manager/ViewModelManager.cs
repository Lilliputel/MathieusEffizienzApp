using LogicLayer.ViewModels;
using LogicLayer.Views;
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

		private static DashboardViewModel _Dashboard;
		private static PlanViewModel _Plan;
		private static GoalOverviewViewModel _GoalOverview;
		private static GanttDiagramViewModel _GanttDiagram;
		private static StatisticsViewModel _Statistics;
		private static SettingsViewModel _Settings;

		private static Dictionary<EnumViewModels, ViewModelBase> _DictionaryViewModelsEssential;

		private static PomodoroViewModel _Pomodoro;
		private static NewCategoryViewModel _NewCategory;
		private static NewGoalViewModel _NewGoal;
		private static NewDayTimeViewModel _NewDayTime;

		#endregion

		#region properties

		public static ViewModelBase? SelectedMainViewModel { get; private set; }
		public static ViewModelBase? SelectedEssentialViewModel { get; private set; }

		#endregion

		#region initializer

		static ViewModelManager() {

			_Dashboard = new DashboardViewModel();
			_Plan = new PlanViewModel();
			_GoalOverview = new GoalOverviewViewModel();
			_GanttDiagram = new GanttDiagramViewModel();
			_Statistics = new StatisticsViewModel();
			_Settings = new SettingsViewModel();

			_DictionaryViewModelsMain = new Dictionary<EnumViewModels, ViewModelBase>() {
				{EnumViewModels.Dashboard, _Dashboard },
				{EnumViewModels.Plan, _Plan },
				{EnumViewModels.GoalOverview, _GoalOverview },
				{EnumViewModels.GanttDiagram, _GanttDiagram },
				{EnumViewModels.Statistics, _Statistics },
				{ EnumViewModels.Settings, _Settings }
			};

			_Pomodoro = new PomodoroViewModel();
			_NewCategory = new NewCategoryViewModel();
			_NewGoal = new NewGoalViewModel();
			_NewDayTime = new NewDayTimeViewModel();

			_DictionaryViewModelsEssential = new Dictionary<EnumViewModels, ViewModelBase>(){
				{ EnumViewModels.Pomodoro, _Pomodoro },
				{EnumViewModels.NewCategory, _NewCategory },
				{EnumViewModels.NewGoal, _NewGoal },
				{EnumViewModels.NewDayTime, _NewDayTime }
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
