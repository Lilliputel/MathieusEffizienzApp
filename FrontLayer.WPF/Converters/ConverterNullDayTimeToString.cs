using ModelLayer.Planning;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(DayTime), typeof(string))]
	public class ConverterNullDayTimeToString : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			if( value is null )
				return "The DayTime is not Overlapping!";
			else if( value is DayTime dayTime )
				return dayTime.ToString();
			else {
				Debug.WriteLine($"Tried to Convert a different Object[{value}] in ConverterNullToString");
				return value;
			}
		}
		public object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
			throw new NotImplementedException();
		}

	}
}
