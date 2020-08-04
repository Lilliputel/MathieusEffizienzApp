using ModelLayer.Classes;
using System.Collections.ObjectModel;
using System.Windows;


namespace FrontLayer.Views {

	public class DashboardViewModel : ViewModelBase {

		#region fields

		#endregion

		#region properties

		public ObservableCollection<Kategorie> Kategorien
			=> ( Application.Current as App ).KategorienListe;

		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion
	}
}
