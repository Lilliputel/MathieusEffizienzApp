using LogicLayer.Manager;
using ModelLayer.Utility;
using System.Windows;

namespace FrontLayer {
	public partial class App : Application {

		#region constructor

		public App() {

			// Load the saved KategorienListe from The XML-File
			XMLHandler.Laden(ObjectManager.KategorienListe, nameof(ObjectManager.KategorienListe));

		}

		#endregion

	}
}
