using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( System.Drawing.Color ), typeof( SolidColorBrush ) )]
	public class DrawingColorToSolidBrush : MarkedupValueConverter<DrawingColorToSolidBrush> {

		public override object? Convert( object? value, Type targetType, object parameter, CultureInfo culture )
			=> new SolidColorBrush( (value as System.Drawing.Color?)?.ToMediaColor() ?? Colors.Transparent );

		public override object? ConvertBack( object? value, Type targetType, object parameter, CultureInfo culture )
			=> (value as SolidColorBrush)?.Color.ToDrawingColor() ?? System.Drawing.Color.Transparent;

	}
}
