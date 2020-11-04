using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {
	[ValueConversion( typeof( double[] ), typeof( double ) )]
	public class DoublesToSize : MarkedupMultiValueConverter<DoublesToSize> {
		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			#region input
			double Factor1 = (double) values[0];
			double Factor2 = (double) values[1];
			#endregion
			return Factor1 * Factor2;
		}
		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture ) => throw new NotImplementedException();
	}
}
