using ModelLayer.Planning;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.Converters {
	public class DayTimeToBorderConverter : IMultiValueConverter {

		public object? Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			// get all the Values from Input
			DayTime time = (DayTime)values[0];
			double start = time.Start;
			double end = time.End;
			double totalHeight = (double)values[1];
			double headerHeight = (double)values[2];
			string mode = (string)parameter;

			double actualHeight = totalHeight - headerHeight;
			#endregion

			#region conversion

			double maxValue = 24;
			double startFactor = start / maxValue;
			double endFactor = end / maxValue;
			double heightFactor = endFactor - startFactor;

			#endregion

			if( mode == "Start" ) {
				return new Thickness(0, startFactor * actualHeight, 0, 0);
			}
			else if( mode == "End" ) {
				return heightFactor * actualHeight;
			}

			return null;

		}
		public object[]? ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture ) {

			return null;

		}

	}
}
