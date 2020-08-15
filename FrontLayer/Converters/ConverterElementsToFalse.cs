using System;
using System.Windows.Data;

namespace FrontLayer.Converters {
	public class ConverterElementsToFalse : IMultiValueConverter {

		public object Convert( object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
			foreach( var item in values ) {
				if( item is string s && s == string.Empty )
					return false;
				if( item is bool b && b == false )
					return false;
				if( item is object o && o == null )
					return false;

			}
			return true;
		}

		public object[] ConvertBack( object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
			throw new ArgumentException();
		}

	}
}
