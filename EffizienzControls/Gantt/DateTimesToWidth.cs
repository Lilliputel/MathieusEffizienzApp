using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( object[] ), typeof( double ) )]
	public class DateTimesToWidth : MarkedupMultiValueConverter<DateTimesToWidth> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			var projektStart = (DateTime)values[0];
			var projektEnde = (DateTime)values[1];
			var datum = (DateTime)values[2];
			double faktor = 0.0;
			#endregion

			#region conversion
			// setzt den faktor zu einem Bruch der gesamtlänge 
			// Datum - StartDatum = Reiner Tagesunterschied a
			// EndDatum - StartDatum = Reiner Tagesunterschied b

			double start = datum.Date.Subtract( projektStart.Date ).TotalDays;
			double end = projektEnde.Date.Subtract( projektStart.Date ).TotalDays;
			if( end > 0 )
				faktor = start / end;
			#endregion

			return faktor;
		}

	}

}
