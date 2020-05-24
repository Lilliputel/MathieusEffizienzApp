using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Effizienz.Utility {

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

		public static void Speichern<T>( List<T> _speicherListe ) {
			Speichern<T>(_speicherListe, DateiName);
		}
		public static void Speichern<T>( List<T> _speicherListe, string _DateiName ) {
			Speichern<T>(_speicherListe, _DateiName, SpeicherPfad);
		}
		public static void Speichern<T>( List<T> _speicherListe, string _DateiName, string _DateiPfad ) {
			DateiNameSetzen(_DateiName);
			try {
				using( FileStream fileStream = new FileStream(_DateiPfad + DateiName, FileMode.Create) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(List<T>));
					Serializer.Serialize(fileStream, _speicherListe);
				}
			}
			catch( FileNotFoundException ) {
				MessageBoxDisplayer.FileNotFound(_DateiName, _DateiPfad);
			}
		}

		public static void Laden<T>( out List<T> _ladeListe ) {
			Laden<T>(out _ladeListe, DateiName );
		}
		public static void Laden<T>( out List<T> _ladeListe, string _DateiName ) {
			Laden<T>(out _ladeListe, _DateiName, SpeicherPfad);
		}
		public static void Laden<T>( out List<T> _ladeListe, string _DateiName, string _DateiPfad ) {
			DateiNameSetzen(_DateiName);
			try {
				using( FileStream fileStream = new FileStream(_DateiPfad + DateiName, FileMode.Open) ) {
					XmlSerializer Serializer = new XmlSerializer(typeof(List<T>));
					_ladeListe = (List<T>)Serializer.Deserialize(fileStream);
				}
			}
			catch( FileNotFoundException ) {
				MessageBoxDisplayer.FileNotFound(_DateiName, _DateiPfad);
				_ladeListe = new List<T>();
			}
			
		}

	}
}
