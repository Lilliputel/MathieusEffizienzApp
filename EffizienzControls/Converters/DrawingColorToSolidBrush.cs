using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EffizienzControls.Converters {

	[ValueConversion(typeof(System.Drawing.Color), typeof(SolidColorBrush))]
	public class DrawingColorToSolidBrush : MarkedupValueConverter<DrawingColorToSolidBrush> {

		public override object Convert( object value, Type targetType, object parameter, CultureInfo culture )
			=> new SolidColorBrush(( (System.Drawing.Color)value ).ToMediaColor());

		public override object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> ( (SolidColorBrush)value ).Color.ToDrawingColor();

	}
}
