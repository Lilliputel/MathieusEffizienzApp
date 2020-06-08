using System.Windows;

namespace Effizienz.Utility {
	public static class MessageBoxDisplayer {

		#region static methods
		
		public static void ObjektErstellt( string _objektName, string _objektTitel ) {
			MessageBox.Show(
				$"Es wurde ein {_objektName} mit dem Titel {_objektTitel} erfolgreich erstellt!",
				"Objekt erstellt!",
				MessageBoxButton.OK);
		}

		public static void ZeitFormatInkorrekt() {
			MessageBox.Show(
				"Bitte ein korrektes Zeitformat verwenden! [hh:mm:ss]",
				"Format anpassen!",
				MessageBoxButton.OK);
		}

		public static void InputInkorrekt( string _exceptionMessage ) {
			MessageBox.Show(
					$"Hier ist etwas Schiefgelaufen! Bitte überprüfe die korrekte Formatierung! \n{_exceptionMessage}",
					"Fehler!",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
		}

		public static void ListeGeladen( string _listenName ) {
			MessageBox.Show(
					$"Die Liste {_listenName} wurde geladen!",
					"Laden erfolgreich!",
					MessageBoxButton.OK);
		}
		public static void ListeGespeichert( string _listenName ) {
			MessageBox.Show(
					$"Die Liste {_listenName} wurde gespeichert!",
					"Speichern erfolgreich!",
					MessageBoxButton.OK);
		}

		public static void FileNotFound( string _dateiName, string _dateiPfad ) {
			MessageBox.Show(
				$"Datei {_dateiName} nicht gefunden! \n{_dateiPfad + _dateiName + ".xml"}",
				"File Not Found!",
				MessageBoxButton.OK,
				MessageBoxImage.Exclamation);
		}

		public static void NullReferenceException( string _exceptionText ) {
			MessageBox.Show(
				$"Das Programm hat den Fehler \n{_exceptionText} \ngeworfen",
				"Fehler!",
				MessageBoxButton.OK,
				MessageBoxImage.Error);
		}

		#endregion


	}
}
