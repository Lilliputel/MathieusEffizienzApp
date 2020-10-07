using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object[]), typeof(Thickness))]
	public class DateTimesToLeftMargin : MarkedupMultiValueConverter<DateTimesToLeftMargin> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			DateTime projektStart = (DateTime)values[0];
			DateTime projektEnde = (DateTime)values[1];
			DateTime datum = (DateTime)values[2];
			double gesamtLänge = (double)values[3];

			double faktor = 0.0;
			// setzt den faktor zu einem Bruch der gesamtlänge 
			// Datum - StartDatum = Reiner Tagesunterschied a
			// EndDatum - StartDatum = Reiner Tagesunterschied b

			double start = datum.Date.Subtract(projektStart.Date).TotalDays;
			double end = projektEnde.Date.Subtract(projektStart.Date).TotalDays;
			if( end > 0 )
				faktor = start / end;

			double position = (faktor * gesamtLänge); // + offset;
			return new Thickness(position, 0, 0, 0);

		}

		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();

	}

}
