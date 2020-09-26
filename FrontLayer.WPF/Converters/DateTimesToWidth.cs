using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object[]), typeof(double))]
	public class DateTimesToWidth : MarkedupMultiValueConverter<DateTimesToWidth> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			DateTime projektStart = (DateTime)values[0];
			DateTime projektEnde = (DateTime)values[1];
			DateTime datum = (DateTime)values[2];
			double gesamtLänge = ((GridLength)values[3]).Value;
			//double offset = ((double)values[4]);

			double faktor;
			// setzt den faktor zu einem Bruch der gesamtlänge 
			// Datum - StartDatum = Reiner Tagesunterschied a
			// EndDatum - StartDatum = Reiner Tagesunterschied b

			try {
				double start = datum.Date.Subtract(projektStart.Date).TotalDays;
				double end = projektEnde.Date.Subtract(projektStart.Date).TotalDays;
				faktor = start / end;
			}
			catch( DivideByZeroException ) {
				faktor = 0;
			}

			return ( faktor * gesamtLänge ); //+ offset;

		}

		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();

	}

}
