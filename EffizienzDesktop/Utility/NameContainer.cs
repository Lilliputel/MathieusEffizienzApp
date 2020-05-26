using Effizienz.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Effizienz.Utility {

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

		private static Dictionary<string, ViewModelBase> dictionaryViewModelsMain = new Dictionary<string, ViewModelBase>();
		private static Dictionary<string, ViewModelBase> dictionaryViewModelsEssential = new Dictionary<string, ViewModelBase>();

		static NameContainer() {
			dictionaryViewModelsMain.Add(nameof(EnumViewModels.Dashboard), new ViewModelDashboard());
			dictionaryViewModelsMain.Add(nameof(EnumViewModels.Planung), new ViewModelPlanung());
			dictionaryViewModelsMain.Add(nameof(EnumViewModels.ProjektÜbersicht), new ViewModelProjektÜbersicht());
			dictionaryViewModelsMain.Add(nameof(EnumViewModels.Gantt), new ViewModelGantt());
			dictionaryViewModelsMain.Add(nameof(EnumViewModels.Statistik), new ViewModelStatistik());
			dictionaryViewModelsMain.Add(nameof(EnumViewModels.Optionen), new ViewModelOptionen());

			dictionaryViewModelsEssential.Add(nameof(EnumViewModels.Pomodoro), new ViewModelPomodoro());
			dictionaryViewModelsEssential.Add(nameof(EnumViewModels.Kategorie), new ViewModelKategorie());
			dictionaryViewModelsEssential.Add(nameof(EnumViewModels.Projekt), new ViewModelProjekt());
			dictionaryViewModelsEssential.Add(nameof(EnumViewModels.Aufgabe), new ViewModelAufgabe());
		}

		public static ViewModelBase GetViewModel( string viewModelName, out bool isMain ) {
			ViewModelBase returnedViewModel;
			if( dictionaryViewModelsMain.TryGetValue(viewModelName, out returnedViewModel) ) {
				isMain = true;
				return returnedViewModel;
			}
			else if( dictionaryViewModelsEssential.TryGetValue(viewModelName, out returnedViewModel) ) {
				isMain = false;
				return returnedViewModel;
			}
			else {
				throw new NotImplementedException();
			}
		}

		public static string GetName( ViewModelBase viewModel ) {
			return dictionaryViewModelsMain.FirstOrDefault(x => x.Value == viewModel).Key ??
				dictionaryViewModelsEssential.FirstOrDefault(x => x.Value == viewModel).Key;
		}

	}
}
