using LogicLayer.ViewModels;
using LogicLayer.Views;
using System.Collections.Generic;
using System.Linq;


namespace LogicLayer.Utility {

	public enum EnumViewModels {

		Dashboard,
		Planung,
		ProjektÜbersicht,
		Gantt,
		Statistik,
		Optionen,

		Pomodoro,
		Kategorie,
		Projekt,
		Aufgabe

	}

	public static class NameContainer {

		#region fields

		private static Dictionary<EnumViewModels, ViewModelBase> dictionaryViewModelsMain;
		private static Dictionary<EnumViewModels, ViewModelBase> dictionaryViewModelsEssential;

		#endregion

		#region static constructor

		static NameContainer() {

			dictionaryViewModelsMain = new Dictionary<EnumViewModels, ViewModelBase>();
			dictionaryViewModelsEssential = new Dictionary<EnumViewModels, ViewModelBase>();

			dictionaryViewModelsMain.Add(EnumViewModels.Dashboard, new DashboardViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.Planung, new PlanungViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.ProjektÜbersicht, new ProjektÜbersichtViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.Gantt, new GanttViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.Statistik, new StatistikViewModel());
			dictionaryViewModelsMain.Add(EnumViewModels.Optionen, new OptionenViewModel());

			dictionaryViewModelsEssential.Add(EnumViewModels.Pomodoro, new PomodoroViewModel());
			dictionaryViewModelsEssential.Add(EnumViewModels.Kategorie, new KategorieViewModel());
			dictionaryViewModelsEssential.Add(EnumViewModels.Projekt, new ProjektViewModel());
			dictionaryViewModelsEssential.Add(EnumViewModels.Aufgabe, new AufgabeViewModel());

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
