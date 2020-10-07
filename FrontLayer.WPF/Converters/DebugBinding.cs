using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;

namespace FrontLayer.WPF.Converters {
	public class DebugBinding : MarkedupValueConverter<DebugBinding> {
		public override object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			return value;
		}
		public override object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
			return value;
		}
	}
}
