using LogicLayer.Extensions;

namespace LogicLayer.Manager {

	public static class AlertManager {

		#region public events

		public static event AlertEventHanlder? AlertOccured;

		#endregion

		#region static methods

		public static void ObjektErstellt( string _objektName, string _objektTitel ) {
			AlertOccured?.Invoke(
				$"Es wurde ein {_objektName} mit dem Titel {_objektTitel} erfolgreich erstellt!",
				"OK",
				"Objekt erstellt!",
				AlertSymbolEnum.Information);
		}

		public static void ZeitFormatInkorrekt() {
			AlertOccured?.Invoke(
				"Bitte ein korrektes Zeitformat verwenden! [hh:mm:ss]",
				"OK",
				"Format anpassen!",
				AlertSymbolEnum.Warning);
		}

		public static void InputInkorrekt( string _exceptionMessage ) {
			AlertOccured?.Invoke(
					$"Hier ist etwas Schiefgelaufen! Bitte überprüfe die korrekte Formatierung! \n{_exceptionMessage}",
					"Anpassen!",
					"Fehler!",
					AlertSymbolEnum.Error);
		}

		public static void ListeGeladen( string _listenName ) {
			AlertOccured?.Invoke(
					$"Die Liste {_listenName} wurde geladen!",
					"OK",
					"Laden erfolgreich!",
					AlertSymbolEnum.OK);
		}
		public static void ListeGespeichert( string _listenName ) {
			AlertOccured?.Invoke(
					$"Die Liste {_listenName} wurde gespeichert!",
					"OK",
					"Speichern erfolgreich!",
					AlertSymbolEnum.OK);
		}

		public static void FileNotFound( string _dateiName, string _dateiPfad ) {
			AlertOccured?.Invoke(
				$"Datei {_dateiName} nicht gefunden! \n{_dateiPfad + _dateiName + ".xml"}",
				"OK",
				"File Not Found!",
				AlertSymbolEnum.Error);
		}

		public static void NullReferenceException( string _exceptionText ) {
			AlertOccured?.Invoke(
				$"Das Programm hat den Fehler \n{_exceptionText} \ngeworfen",
				"OK",
				"Fehler!",
				AlertSymbolEnum.Fatal);
		}

		#endregion

	}

}
