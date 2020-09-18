using FrontLayer.WPF.Extensions;
using System;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object[]), typeof(bool))]
	public class ConverterElementsToFalse : MarkedupMultiValueConverter<ConverterElementsToFalse> {

		public override object Convert( object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
			foreach( var item in values ) {
				if( item is string s && s == string.Empty )
					return false;
				if( item is bool b && b is false )
					return false;
				if( item is null )
					return false;
			}
			return true;
		}

		public override object[] ConvertBack( object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture )
			=> throw new ArgumentException();

	}
}
