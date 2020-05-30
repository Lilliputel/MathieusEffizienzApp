using Effizienz.Classes;
using Effizienz.Utility;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace Effizienz {

	public partial class App : Application {

		#region fields

		public static CultureInfo zeitformat = new CultureInfo("ch-DE");

		private ResourceDictionary themeDark;
		private ResourceDictionary themeLight;
		private string themeDirectory = "/Effizienz;component/Themes/";
		private bool DarkMode;

		#endregion

		#region properties

		public ObservableCollection<Kategorie> KategorienListe { get; set; } 

		#endregion

		#region constructor

		public App() {
			themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute ) };
			themeLight = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute) };
			DarkMode = false;

			KategorienListe = new ObservableCollection<Kategorie>();

			ObservableCollection<Kategorie> transferListe;
			XMLHandler.Laden(out transferListe, nameof(KategorienListe));
			foreach( Kategorie item in transferListe ) {
				KategorienListe.Add(item);
			}
			
		}

		#endregion

		#region methods

		public void SwitchTheme() {
			DarkMode = !DarkMode;
			SetDarkMode(DarkMode);
		}

		private void SetDarkMode( bool _DarkMode ) {

			Resources.MergedDictionaries[0].MergedDictionaries.Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add( _DarkMode ? themeDark : themeLight );
		}

		public bool GetDarkMode() {
			return Resources.MergedDictionaries[0].MergedDictionaries.Contains(this.themeDark);
		}

		public void Speichern() {
			XMLHandler.Speichern(KategorienListe, nameof(KategorienListe));
			MessageBoxDisplayer.ListeGespeichert(nameof(KategorienListe));
		}

		#endregion
		
	}
}
