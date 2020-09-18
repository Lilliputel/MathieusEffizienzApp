using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(Color), typeof(SolidColorBrush))]
	public class ConverterWindowsColorToSolidColorBrush : MarkedupValueConverter<ConverterWindowsColorToSolidColorBrush> {

		public override object Convert( object value, Type targetType, object parameter, CultureInfo culture )
			=> new SolidColorBrush((Color)value);

		public override object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> ( value as SolidColorBrush )?.Color;

	}
}
