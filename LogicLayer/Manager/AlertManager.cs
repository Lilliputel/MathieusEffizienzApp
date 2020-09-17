using LogicLayer.Extensions;

namespace LogicLayer.Manager {

	public static class AlertManager {

		#region public events

		public static event AlertEventHanlder? AlertOccured;

		#endregion

		#region private methods

		private static void OnAlertOccured( string message, string buttonText, string? title, AlertSymbolEnum? symbol ) {
			AlertOccured?.Invoke(message, buttonText, title, symbol);
		}

		#endregion

		#region static methods

		public static void ObjektErstellt( string _objektName, string _objektTitel ) {
			OnAlertOccured(
				$"Es wurde ein {_objektName} mit dem Titel {_objektTitel} erfolgreich erstellt!",
				"OK",
				"Objekt erstellt!",
				AlertSymbolEnum.Information);
		}

		public static void ZeitFormatInkorrekt() {
			OnAlertOccured(
				"Bitte ein korrektes Zeitformat verwenden! [hh:mm:ss]",
				"OK",
				"Format anpassen!",
				AlertSymbolEnum.Warning);
		}

		public static void InputInkorrekt( string _exceptionMessage ) {
			OnAlertOccured(
					$"Hier ist etwas Schiefgelaufen! Bitte überprüfe die korrekte Formatierung! \n{_exceptionMessage}",
					"Anpassen!",
					"Fehler!",
					AlertSymbolEnum.Error);
		}

		public static void ListeGeladen( string _listenName ) {
			OnAlertOccured(
					$"Die Liste {_listenName} wurde geladen!",
					"OK",
					"Laden erfolgreich!",
					AlertSymbolEnum.OK);
		}
		public static void ListeGespeichert( string _listenName ) {
			OnAlertOccured(
					$"Die Liste {_listenName} wurde gespeichert!",
					"OK",
					"Speichern erfolgreich!",
					AlertSymbolEnum.OK);
		}

		public static void FileNotFound( string _dateiName, string _dateiPfad ) {
			OnAlertOccured(
				$"Datei {_dateiName} nicht gefunden! \n{_dateiPfad + _dateiName + ".xml"}",
				"OK",
				"File Not Found!",
				AlertSymbolEnum.Error);
		}

		public static void NullReferenceException( string _exceptionText ) {
			OnAlertOccured(
				$"Das Programm hat den Fehler \n{_exceptionText} \ngeworfen",
				"OK",
				"Fehler!",
				AlertSymbolEnum.Fatal);
		}

		#endregion

	}

}
