using EffizienzControls.Extensions;
using System;
using System.Diagnostics;
using System.Globalization;

namespace EffizienzControls.Converters {

	public class DebugBinding : MarkedupValueConverter<DebugBinding> {

		public override object? Convert( object? value, Type targetType, object parameter, CultureInfo culture ) {
			Debugger.Break();
			return value;
		}
		public override object? ConvertBack( object? value, Type targetType, object parameter, CultureInfo culture ) {
			Debugger.Break();
			return value;
		}

	}
}
