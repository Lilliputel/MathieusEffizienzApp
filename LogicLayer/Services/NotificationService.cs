using LogicLayer.Extensions;

namespace LogicLayer.Services {

	public static class NotificationService {

		#region public events
		public static event AlertEventHanlder? AlertOccured;
		private static void RaiseAlertOccured( string message, string buttonText, string? title, AlertSymbolEnum? symbol )
			=> AlertOccured?.Invoke( message, buttonText, title, symbol );
		#endregion

		#region static methods
		public static void ObjektErstellt( string objektName, string objektTitel )
			=> RaiseAlertOccured(
				$"Es wurde ein {objektName} mit dem Titel {objektTitel} erfolgreich erstellt!",
				"OK",
				"Objekt erstellt!",
				AlertSymbolEnum.Information );
		public static void InputInkorrekt( string exceptionMessage )
			=> RaiseAlertOccured(
					$"Hier ist etwas Schiefgelaufen! Bitte überprüfe die korrekte Formatierung! \n{exceptionMessage}",
					"Anpassen!",
					"Fehler!",
					AlertSymbolEnum.Error );
		public static void ListeGeladen( string listenName )
			=> RaiseAlertOccured(
					$"Die Liste {listenName} wurde geladen!",
					"OK",
					"Laden erfolgreich!",
					AlertSymbolEnum.OK );
		public static void ListeGespeichert( string listenName )
			=> RaiseAlertOccured(
					$"Die Liste {listenName} wurde gespeichert!",
					"OK",
					"Speichern erfolgreich!",
					AlertSymbolEnum.OK );
		public static void FileNotFound( string dateiName, string dateiPfad )
			=> RaiseAlertOccured(
				$"Datei {dateiName} nicht gefunden! \n{dateiPfad + dateiName + ".xml"}",
				"OK",
				"File Not Found!",
				AlertSymbolEnum.Error );
		#endregion

	}

}
