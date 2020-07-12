using Effizienz.Classes;
using Effizienz.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace UiLayer {
	public partial class App : Application {

		#region fields

		public static CultureInfo zeitformat = new CultureInfo("ch-DE");

		private string themeDirectory = "/FrontLayer;component/Themes/";
		private ResourceDictionary themeDark;
		private ResourceDictionary themeLight;
		private bool DarkMode;

		private ObservableCollection<Kategorie> kategorienListe;

		#endregion

		#region properties

		/// <summary>
		/// Die Globale KategorienListe, welche gespeichert wird und alle Kategorien enthalten Sollte.
		/// </summary>
		public ObservableCollection<Kategorie> KategorienListe {
			get { return kategorienListe; }
			set { kategorienListe = value; }
		}

		/// <summary>
		/// Das Globale Dictionary, in welchem alle Zeiten mit der zugehörigen Kategorie erfasst sind.
		/// </summary>
		public Dictionary<DateTime, Guid> Stundenplan { get; set; }

		#endregion

		#region constructor

		public App() {
			// Initialize the Themes
			themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute) };
			themeLight = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute) };
			DarkMode = false;

			// Initialize KategorienListe and Stundenplan
			KategorienListe = new ObservableCollection<Kategorie>();
			Stundenplan = new Dictionary<DateTime, Guid>();

			// Load the saved KategorienListe from The XML-File
			XMLHandler.Laden(out kategorienListe, nameof(KategorienListe));

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

		public void GenerateObjects() {
			Kategorie CBKategorie = new Kategorie("CodeBehind-Kategorie", Colors.Magenta);
			Projekt CBProjekt = new Projekt("CodeBehind-Projekt", CBKategorie.ID, DateTime.Today.AddDays(1), DateTime.Today.AddDays(10)){
				Zeit = new TimeSpan(1, 2, 3)
			};
			Aufgabe CBAufgabe = new Aufgabe("CodeBehind-Aufgabe", CBProjekt.ID, DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)){
				Zeit = new TimeSpan(3, 12, 20)
			};

			CBProjekt.Aufgaben.Add(CBAufgabe);
			CBKategorie.Projekte.Add(CBProjekt);
			this.KategorienListe.Add(CBKategorie);
		}

		#endregion

	}
}
