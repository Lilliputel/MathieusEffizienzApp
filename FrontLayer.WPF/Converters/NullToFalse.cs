using FrontLayer.WPF.Extensions;
using System;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object), typeof(bool))]
	public class NullToFalse : MarkedupValueConverter<NullToFalse> {

		public override object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
			=> value is { };

#pragma warning disable CS8764 // Nullability of return type doesn't match overridden member (possibly because of nullability attributes).
		public override object? ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
			=> null;
#pragma warning restore CS8764 // Nullability of return type doesn't match overridden member (possibly because of nullability attributes).

	}
}
