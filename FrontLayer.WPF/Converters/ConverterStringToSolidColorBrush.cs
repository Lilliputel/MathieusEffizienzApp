using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(string), typeof(SolidColorBrush))]
	public class ConverterStringToSolidColorBrush : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			Color? farbe = (Color)ColorConverter.ConvertFromString(value.ToString());
			return new SolidColorBrush(farbe ?? Colors.White);
		}

		public object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
			return ( value as SolidColorBrush )?.Color.ToString();
		}

	}
}
