using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion(typeof(double), typeof(double))]
	public class DivisionWithParameter : MarkedupValueConverter<DivisionWithParameter> {

		public override object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			double.TryParse((string)parameter, out double divisor);
			double result = 0.0;
			try {
				var dividend = (double)value;
				result = dividend / divisor;
			}
			catch( DivideByZeroException ) {
				throw;
			}
			return result;
		}
		public override object ConvertBack( object value, Type targetTypes, object parameter, CultureInfo culture ) {
			var dividend = (double)value;
			double.TryParse((string)parameter, out double divisor);
			return dividend * divisor;
		}

	}
}
