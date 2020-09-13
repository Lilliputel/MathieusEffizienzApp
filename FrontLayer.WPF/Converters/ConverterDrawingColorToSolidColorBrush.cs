using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(System.Drawing.Color), typeof(SolidColorBrush))]
	public class ConverterDrawingColorToSolidColorBrush : IValueConverter {
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
			=> new SolidColorBrush(( (System.Drawing.Color)value ).ToMediaColor());

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> ( (SolidColorBrush)value ).Color.ToDrawingColor();

	}
}
