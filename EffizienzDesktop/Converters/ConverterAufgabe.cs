using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Effizienz.Converters {
	public class ConverterAufgabe : IMultiValueConverter {

		public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			DateTime projektStart = (DateTime)values[0];
			DateTime projektEnde = (DateTime)values[1];
			DateTime datum = (DateTime)values[2];
			double gesamtLänge = (double)values[3];

			double faktor;
			// setzt den faktor zu einem Bruch der gesamtlänge
			try { faktor = datum.Date.Subtract(projektStart.Date).TotalDays / projektEnde.Date.Subtract(projektStart.Date).TotalDays;
			}
			catch( DivideByZeroException ) {
				faktor = 0;
			}
			
			double position = faktor * gesamtLänge;

			if( targetType == typeof(Thickness) ) {
				return new Thickness(position, 0, 0, 0);
			}
			else {
				return position;
			}

		}
		public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture ) { 
			throw new NotImplementedException(); 
		}
	}
}
