using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(string), typeof(SolidColorBrush))]
	public class ConverterSolidColorBrushToColor : MarkedupValueConverter<ConverterSolidColorBrushToColor> {

		public override object? Convert( object value, Type targetType, object parameter, CultureInfo culture )
			=> ( value as SolidColorBrush )?.Color;

		public override object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> new SolidColorBrush((Color)value);

	}
}
