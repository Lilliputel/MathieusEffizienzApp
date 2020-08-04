using ModelLayer.Classes;
using System.Collections.ObjectModel;
using System.Windows;

namespace FrontLayer.Views {
	public class ProjektÜbersichtViewModel : ViewModelBase {

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
