using FrontLayer.WPF.Extensions;
using ModelLayer.Planning;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(DoubleTime), typeof(Thickness))]
	public class ConverterDayTimeToThickness : MarkedupMultiValueConverter<ConverterDayTimeToThickness> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			// get all the Values from Input
			DoubleTime time = (DoubleTime)values[0];
			double start = time.Start;
			double end = time.End;
			double height = (double)values[1];

			string mode = (string)parameter;

			#endregion

			#region conversion

			double maxValue = 24;
			double startFactor = start / maxValue;

			#endregion

			return new Thickness(0, startFactor * height, 0, 0);


		}

		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();

	}
}
