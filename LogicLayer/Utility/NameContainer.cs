using LogicLayer.ViewModels;
using LogicLayer.Views;
using System.Collections.Generic;
using System.Linq;


namespace LogicLayer.Utility {

	public enum EnumViewModels {

		Dashboard,
		Plan,
		GoalOverview,
		GanttDiagram,
		Statistics,
		Settings,

		Pomodoro,
		NewCategory,
		NewGoal

	}

	public static class NameContainer {

		#region fields

		private static Dictionary<EnumViewModels, ViewModelBase> dictionaryViewModelsMain;
		private static Dictionary<EnumViewModels, ViewModelBase> dictionaryViewModelsEssential;

		#endregion

		#region initializer

		static NameContainer() {

			dictionaryViewModelsMain = new Dictionary<EnumViewModels, ViewModelBase>();
			dictionaryViewModelsEssential = new Dictionary<EnumViewModels, ViewModelBase>();

			dictionaryViewModelsMain.Add(EnumViewModels.Dashboard, new DashboardViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.Plan, new PlanViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.GoalOverview, new GoalOverviewViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.GanttDiagram, new GanttDiagramViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.Statistics, new StatisticsViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.Settings, new SettingsViewModel());

			dictionaryViewModelsEssential.Add(EnumViewModels.Pomodoro, new PomodoroViewModel());
			dictionaryViewModelsEssential.Add(EnumViewModels.NewCategory, new NewCategoryViewModel());
			dictionaryViewModelsEssential.Add(EnumViewModels.NewGoal, new NewGoalViewModel());

		}

		#endregion

		#region methods

		public static ViewModelBase GetViewModel( EnumViewModels viewModelName ) {
			return
				dictionaryViewModelsMain.GetValueOrDefault(viewModelName) ??
				dictionaryViewModelsEssential.GetValueOrDefault(viewModelName);
		}

		public static ViewModelBase GetViewModel( EnumViewModels viewModelName, out bool isMain ) {
			ViewModelBase returnedViewModel;
			if( dictionaryViewModelsMain.TryGetValue(viewModelName, out returnedViewModel) ) {
				isMain = true;
			}
			else {
				dictionaryViewModelsEssential.TryGetValue(viewModelName, out returnedViewModel);
				isMain = false;
			}
			return returnedViewModel;
		}

		public static string GetName( ViewModelBase viewModel ) {
			return
				dictionaryViewModelsMain.FirstOrDefault(x => x.Value == viewModel).Key.ToString() ??
				dictionaryViewModelsEssential.FirstOrDefault(x => x.Value == viewModel).Key.ToString();
		}

		#endregion

	}
}
