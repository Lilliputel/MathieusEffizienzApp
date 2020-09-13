using ModelLayer.Planning;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(DayTime), typeof(Thickness))]
	public class ConverterDayTimeToThickness : IMultiValueConverter {

		public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			// get all the Values from Input
			DayTime time = (DayTime)values[0];
			double start = time.Start;
			double end = time.End;
			double height = (double)values[1];

			string mode = (string)parameter;

			#endregion

			#region conversion

			double maxValue = 24;
			double startFactor = start / maxValue;
			double endFactor = end / maxValue;
			double heightFactor = endFactor - startFactor;

			#endregion

			return new Thickness(0, startFactor * height, 0, 0);


		}
		public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture ) {

			throw new NotImplementedException();

		}

	}
}
