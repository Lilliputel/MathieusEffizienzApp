using System.Globalization;

namespace LogicLayer.Manager {
	public static class CultureManager {

		public static CultureInfo CurrentCulture = new CultureInfo("ch-DE");

		public static void SetCulture( CultureInfo culture ) {
			CurrentCulture = culture;
		}

	}
}
