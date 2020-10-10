using EffizienzControls.Extensions;
using ModelLayer.Classes;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion(typeof(object[]), typeof(Thickness))]
	public class DayTimeToTopMargin : MarkedupMultiValueConverter<DayTimeToTopMargin> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			DoubleTime time = (DoubleTime)values[0];
			double start = time.Start;
			double height = (double)values[1];
			double maxValue = 24;
			#endregion

			#region conversion
			double startFactor = start / maxValue;
			#endregion

			return new Thickness(0, startFactor * height, 0, 0);
		}

		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();

	}
}
