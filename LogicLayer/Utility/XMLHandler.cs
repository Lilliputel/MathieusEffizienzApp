using LogicLayer.Utility;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace ModelLayer.Utility {

	public static class XMLHandler {

		#region fields

		private static string SpeicherPfad { get; set; } = @"S:\TESTING\Effizienz\";
		private static string DateiName { get; set; } = "Test.xml";

		#endregion

		#region methods

		public static void PfadSetzen( string _SpeicherPfad ) {
			SpeicherPfad = _SpeicherPfad;
		}
		public static void DateiNameSetzen( string _DateiName ) {
			if( _DateiName.EndsWith(".xml") ) {
				DateiName = _DateiName;
			}
			else {
				DateiName = string.Concat(_DateiName, ".xml");
			}
		}

		#endregion

		#region speichern

		public static void Speichern<T>( ObservableCollection<T> _speicherListe ) {
			Speichern<T>(_speicherListe, DateiName, SpeicherPfad);
		}
		public static void Speichern<T>( ObservableCollection<T> _speicherListe, string _DateiName ) {
			Speichern<T>(_speicherListe, _DateiName, SpeicherPfad);
		}
		public static void Speichern<T>( ObservableCollection<T> _speicherListe, string _DateiName, string _DateiPfad ) {
			DateiNameSetzen(_DateiName);
			try {
				using( FileStream fileStream = new FileStream(_DateiPfad + DateiName, FileMode.Create) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<T>));
					Serializer.Serialize(fileStream, _speicherListe);
				}
			}
			catch( FileNotFoundException ) {
				MessageBoxDisplayer.FileNotFound(_DateiName, _DateiPfad);
			}
		}

		#endregion

		#region laden

		public static void Laden<T>( ObservableCollection<T> _ladeListe ) {
			Laden<T>(_ladeListe, DateiName, SpeicherPfad);
		}
		public static void Laden<T>( ObservableCollection<T> _ladeListe, string _DateiName ) {
			Laden<T>(_ladeListe, _DateiName, SpeicherPfad);
		}
		public static void Laden<T>( ObservableCollection<T> _ladeListe, string _DateiName, string _DateiPfad ) {
			DateiNameSetzen(_DateiName);
			try {
				using( FileStream fileStream = new FileStream(_DateiPfad + DateiName, FileMode.Open) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(ObservableCollection<T>));
					_ladeListe = new ObservableCollection<T>((ObservableCollection<T>)Serializer.Deserialize(fileStream));
				}
			}
			catch( FileNotFoundException ) {
				MessageBoxDisplayer.FileNotFound(_DateiName, _DateiPfad);
			}
		}

		#endregion
	}
}
