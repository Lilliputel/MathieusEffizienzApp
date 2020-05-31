using Effizienz.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

		#region fields

		private static Dictionary<EnumViewModels, ViewModelBase> dictionaryViewModelsMain;
		private static Dictionary<EnumViewModels, ViewModelBase> dictionaryViewModelsEssential;

		#endregion

		#region static constructor

		static NameContainer() {

			dictionaryViewModelsMain = new Dictionary<EnumViewModels, ViewModelBase>();
			dictionaryViewModelsEssential = new Dictionary<EnumViewModels, ViewModelBase>();

			dictionaryViewModelsMain.Add(EnumViewModels.Dashboard, new ViewModelDashboard());
			dictionaryViewModelsMain.Add(EnumViewModels.Planung, new ViewModelPlanung());
			dictionaryViewModelsMain.Add(EnumViewModels.ProjektÜbersicht, new ViewModelProjektÜbersicht());
			dictionaryViewModelsMain.Add(EnumViewModels.Gantt, new ViewModelGantt());
			dictionaryViewModelsMain.Add(EnumViewModels.Statistik, new ViewModelStatistik());
			dictionaryViewModelsMain.Add(EnumViewModels.Optionen, new ViewModelOptionen());

			dictionaryViewModelsEssential.Add(EnumViewModels.Pomodoro, new ViewModelPomodoro());
			dictionaryViewModelsEssential.Add(EnumViewModels.Kategorie, new ViewModelKategorie());
			dictionaryViewModelsEssential.Add(EnumViewModels.Projekt, new ViewModelProjekt());
			dictionaryViewModelsEssential.Add(EnumViewModels.Aufgabe, new ViewModelAufgabe());

		}

		#endregion

		#region methods
		
		public static ViewModelBase GetViewModel( EnumViewModels viewModelName) {
			ViewModelBase returnedViewModel = dictionaryViewModelsMain.GetValueOrDefault(viewModelName) ??	dictionaryViewModelsEssential.GetValueOrDefault(viewModelName);
			return returnedViewModel;
		}

		public static ViewModelBase GetViewModel( EnumViewModels viewModelName, out bool isMain ) {
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
			return dictionaryViewModelsMain.FirstOrDefault(x => x.Value == viewModel).Key.ToString() ??
				dictionaryViewModelsEssential.FirstOrDefault(x => x.Value == viewModel).Key.ToString();
		}

		#endregion
		
	}
}
