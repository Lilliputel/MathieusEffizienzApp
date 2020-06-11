using Effizienz.Classes;
using Effizienz.Utility;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Effizienz {

	public partial class App : Application {

		#region fields

		private ResourceDictionary themeDark;
		private ResourceDictionary themeLight;
		private string themeDirectory = "/Effizienz;component/Themes/";
		private bool DarkMode;

		#endregion

		#region properties

		public static CultureInfo zeitformat = new CultureInfo("ch-DE");
		public ObservableCollection<Kategorie> KategorienListe { get; set; }

		#endregion

		#region constructor

		public App() {
			themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute) };
			themeLight = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute) };
			DarkMode = false;

			KategorienListe = new ObservableCollection<Kategorie>();

			ObservableCollection<Kategorie> transferListe;
			XMLHandler.Laden(out transferListe, nameof(KategorienListe));
			foreach( Kategorie item in transferListe ) {
				KategorienListe.Add(item);
			}

			GenerateObjects();

		}

		#endregion

		#region methods

		public void SwitchTheme() {
			DarkMode = !DarkMode;
			SetDarkMode(DarkMode);
		}

		private void SetDarkMode( bool _DarkMode ) {

			Resources.MergedDictionaries[0].MergedDictionaries.Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add(_DarkMode ? themeDark : themeLight);
		}

		public bool GetDarkMode() {
			return Resources.MergedDictionaries[0].MergedDictionaries.Contains(this.themeDark);
		}

		public void Speichern() {
			XMLHandler.Speichern(KategorienListe, nameof(KategorienListe));
			MessageBoxDisplayer.ListeGespeichert(nameof(KategorienListe));
		}

		private void GenerateObjects() {
			Kategorie CBKategorie = new Kategorie("CodeBehind-Kategorie", Colors.Magenta);
			Projekt CBProjekt = new Projekt("CodeBehind-Projekt", CBKategorie.ID, DateTime.Today.AddDays(1), DateTime.Today.AddDays(10));
			Aufgabe CBAufgabe = new Aufgabe("CodeBehind-Aufgabe", CBProjekt.ID, DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5));

			CBProjekt.Aufgaben.Add(CBAufgabe);
			CBKategorie.Projekte.Add(CBProjekt);
			this.KategorienListe.Add(CBKategorie);
		}

		#endregion

	}
}
