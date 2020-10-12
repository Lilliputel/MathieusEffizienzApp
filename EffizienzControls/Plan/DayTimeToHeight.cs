using EffizienzControls.Extensions;
using ModelLayer.Classes;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion(typeof(object[]), typeof(double))]
	public class DayTimeToHeight : MarkedupMultiValueConverter<DayTimeToHeight> {

#warning sometimes Time is an Unset value ?!
		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			DoubleTime time = (DoubleTime)values[0];
			double start = time.Start;
			double end = time.End;
			double height = (double)values[1];
			double maxValue = 24;
			#endregion

			#region conversion
			double startFactor = start / maxValue;
			double endFactor = end / maxValue;
			double heightFactor = endFactor - startFactor;
			#endregion

			return heightFactor * height;
		}

		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();

	}
}
