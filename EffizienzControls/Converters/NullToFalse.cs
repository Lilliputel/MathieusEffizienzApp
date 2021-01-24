using EffizienzControls.Extensions;
using System;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( object ), typeof( bool ) )]
	public class NullToFalse : MarkedupValueConverter<NullToFalse> {

		public override object? Convert( object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
			=> value is { };

	}
}
