using LogicLayer.Manager;
using ModelLayer.Utility;
using System.Windows;

namespace FrontLayer {
	public partial class App : Application {

		#region constructor

		public App() {

			// Load the saved Categories from The XML-File
			XMLHandler.Laden(ObjectManager.CategoryList, nameof(ObjectManager.CategoryList));

		}

		#endregion

		#region methods

		protected override void OnStartup( StartupEventArgs e ) {
			Resources.MergedDictionaries[0].Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add(ThemeManager.SelectedTheme);
			base.OnStartup(e);
		}

#warning Speicher-Funktion

		//public static void Speichern() {
		//	XMLHandler.Speichern(ObjectManager.Categories, nameof(ObjectManager.Categories));
		//	MessageBoxDisplayer.ListeGespeichert(nameof(ObjectManager.Categories));
		//}

		#endregion

	}
}
