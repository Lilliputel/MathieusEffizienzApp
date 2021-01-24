using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EffizienzControls.Converters {

#warning test it
	[ValueConversion( typeof( GridLength ), typeof( double ) )]
	public class GridLengthToDouble : MarkedupValueConverter<GridLengthToDouble> {
		public override object? Convert( object? value, Type targetType, object parameter, CultureInfo culture ) {
			if( value is GridLength gl )
				return gl.Value;
			else
				throw new ArgumentException( "The first value must be a GridLength!", nameof( value ) );
		}
		public override object? ConvertBack( object? value, Type targetType, object parameter, CultureInfo culture ) {
			if( value is double d )
				return new GridLength( d );
			else
				throw new ArgumentException( "The first value must be a double!", nameof( value ) );
		}
	}
}
