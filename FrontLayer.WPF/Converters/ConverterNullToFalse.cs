using FrontLayer.WPF.Extensions;
using System;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object), typeof(bool))]
	public class ConverterNullToFalse : MarkedupValueConverter<ConverterNullToFalse> {

		public override object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
			=> value is { };

		public override object? ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
			=> null;
	}
}
