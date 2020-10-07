using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(double[]), typeof(double))]
	public class DoubleSubtraction : MarkedupMultiValueConverter<DoubleSubtraction> {
		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			double subtrahend = (double)values[0];
			double minuend = (double)values[1];
			return subtrahend - minuend;
		}
		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture ) => throw new NotImplementedException();
	}
}
