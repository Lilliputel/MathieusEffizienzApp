using Effizienz.Classes;
using Effizienz.Interfaces;
using Effizienz.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Effizienz {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		public static CultureInfo zeitformat = new CultureInfo("ch-DE");

		private ResourceDictionary themeDark;
		private ResourceDictionary themeLight;
		private string themeDirectory = "/Effizienz;component/Themes/";
		private bool DarkMode;

		public App() {
			themeDark = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeDark.xaml", UriKind.RelativeOrAbsolute ) };
			themeLight = new ResourceDictionary() { Source = new Uri(themeDirectory + "ThemeLight.xaml", UriKind.RelativeOrAbsolute) };

			Laden(ListContainer.KategorienListe, nameof(ListContainer.KategorienListe));
			Laden(ListContainer.ProjektListe, nameof(ListContainer.ProjektListe));
			Laden(ListContainer.AufgabenListe, nameof(ListContainer.AufgabenListe));

			DarkMode = false;
		}

		public void SwitchTheme() {
			DarkMode = !DarkMode;
			SetDarkMode(DarkMode);
		}

		public void SetDarkMode( bool _DarkMode ) {
			Resources.MergedDictionaries[0].MergedDictionaries.Clear();
			Resources.MergedDictionaries[0].MergedDictionaries.Add( _DarkMode ? themeDark : themeLight );
		}

		public bool GetDarkMode() {
			return Resources.MergedDictionaries[0].MergedDictionaries.Contains(this.themeDark);
		}

		private void Laden<T>( ObservableCollection<T> _inputListe, string _listenName ) {
			ObservableCollection<T> neueListe;
			XMLHandler.Laden(out neueListe, _listenName);
			foreach( T item in neueListe ) {
				_inputListe.Add(item);
			}
		}
		public void Speichern<T>( ObservableCollection<T> _inputListe, string _listenName ) {
			XMLHandler.Speichern(_inputListe, _listenName);
			MessageBoxDisplayer.ListeGespeichert(_listenName);
		}

		
	}
}
