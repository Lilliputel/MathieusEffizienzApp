using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace EffizienzNeu.Utility {
	public static class MessageBoxDisplayer {

		public static void ObjektErstellt( string _objektName, string _objektTitel ) {
			MessageBox.Show(
				$"Es wurde ein {_objektName} mit dem Titel {_objektTitel} erfolgreich erstellt!", 
				"Objekt erstellt!", 
				MessageBoxButton.OK );
		}

		public static void ZeitFormatInkorrekt() {
			MessageBox.Show(
				"Bitte ein korrektes Zeitformat verwenden! [hh:mm:ss]", 
				"Format anpassen!", 
				MessageBoxButton.OK );
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

	}
}
