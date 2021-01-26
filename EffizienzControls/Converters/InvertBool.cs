using EffizienzControls.Extensions;
using System;
using System.Globalization;

namespace EffizienzControls.Converters {
	public class InvertBool : MarkedupValueConverter<InvertBool> {
		public override object? Convert( object? value, Type targetType, object parameter, CultureInfo culture )
			=> !(value as bool?);
	}
}
