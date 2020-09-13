using System;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object), typeof(bool))]
	public class ConverterNullToFalse : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			return value is { };
		}

		public object? ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			return null;
		}
	}
}
