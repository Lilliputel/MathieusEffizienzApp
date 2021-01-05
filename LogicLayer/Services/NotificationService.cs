using LogicLayer.Extensions;

namespace LogicLayer.Services {

	public static class NotificationService {

#warning Refactor this!

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
		#endregion

	}

}
