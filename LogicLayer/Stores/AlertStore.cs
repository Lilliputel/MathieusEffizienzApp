using LogicLayer.Extensions;
using System;

namespace LogicLayer.Stores {

	public class AlertStore {

		#region Alert-Action
		public Action<string, string, AlertSymbolEnum> AlertOccured { get; init; }
		#endregion

		#region constructor
		public AlertStore( Action<string, string, AlertSymbolEnum> alertAction )
			=> AlertOccured = alertAction;
		#endregion

		#region public methods
		public void ObjektErstellt( string objektName, string objektTitel )
			=> AlertOccured?.Invoke(
				$"Es wurde ein {objektName} mit dem Titel {objektTitel} erfolgreich erstellt!",
				"Objekt erstellt!",
				AlertSymbolEnum.Information );
		#endregion

	}

}
