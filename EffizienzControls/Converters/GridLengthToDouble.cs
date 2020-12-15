using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( GridLength ), typeof( double ) )]
	public class GridLengthToDouble : MarkedupValueConverter<GridLengthToDouble> {

		public override object? Convert( object? value, Type targetType, object parameter, CultureInfo culture )
			=> (double)((value as GridLength?)?.Value ?? 0.0);

		public override object? ConvertBack( object? value, Type targetType, object parameter, CultureInfo culture )
			=> new GridLength( value as double? ?? 0.0 );

	}
}
