using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.ObjectModel;

namespace LogicLayer.Views {
	public class GanttViewModel : ViewModelBase {

		#region properties

		public ObservableCollection<Kategorie> Kategorien
			=> ObjectManager.KategorienListe;

		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion

	}
}
