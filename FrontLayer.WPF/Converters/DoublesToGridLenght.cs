using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(double[]), typeof(GridLength))]
	public class DoublesToGridLenght : MarkedupMultiValueConverter<DoublesToGridLenght> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			double subtrahend = (double)values[0];
			double minuend = (double)values[1];
			return new GridLength(subtrahend - minuend);

		}
		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
