using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace EffizienzNeu.Utility {

	public static class XMLHandler {

		private static string SpeicherPfad { get; set; } = @"S:\TESTING\Effizienz\";
		private static string DateiName { get; set; } = "Test.xml";

		public static void PfadSetzen( string _SpeicherPfad ) {
			SpeicherPfad = _SpeicherPfad;
		}
		public static void DateiNameSetzen( string _DateiName ) {
			if( _DateiName.EndsWith(".xml")) {
				DateiName = _DateiName;
			}
			else {
				DateiName = string.Concat(_DateiName, ".xml");
			}
		}

		public static void Speichern<T>( ObservableCollection<T> SpeicherListe ) {
			Speichern<T>(SpeicherListe, DateiName);
		}
		public static void Speichern<T>( ObservableCollection<T> SpeicherListe, string _DateiName ) {
			Speichern<T>(SpeicherListe, _DateiName, SpeicherPfad);
		}
		public static void Speichern<T>( ObservableCollection<T> SpeicherListe, string _DateiName, string _DateiPfad ) {
			DateiNameSetzen(_DateiName);
			using(FileStream fileStream = new FileStream(_DateiPfad + DateiName, FileMode.Create) ) {
				try {
					XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<T>));
					Serializer.Serialize(fileStream, SpeicherListe);
				}
				catch( FileNotFoundException ) {
					MessageBox.Show($"Datei {_DateiName} nicht gefunden! \n{_DateiPfad + DateiName}", "File Not Found!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
		}

		public static void Laden<T>( out ObservableCollection<T> LadeListe ) {
			Laden<T>(out LadeListe, DateiName );
		}
		public static void Laden<T>( out ObservableCollection<T> LadeListe, string _DateiName ) {
			Laden<T>(out LadeListe, _DateiName, SpeicherPfad);
		}
		public static void Laden<T>( out ObservableCollection<T> LadeListe, string _DateiName, string _DateiPfad ) {
			DateiNameSetzen(_DateiName);
			using( FileStream fileStream = new FileStream(_DateiPfad + DateiName, FileMode.Open) ) {
				try {
					XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<T>));
					LadeListe = (ObservableCollection<T>)Serializer.Deserialize(fileStream);
				}
				catch( FileNotFoundException ) {
					MessageBox.Show($"Datei {_DateiName} nicht gefunden! \n{_DateiPfad + DateiName}", "File Not Found!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					LadeListe = new ObservableCollection<T>();				
				}
			}
		}

	}
}
