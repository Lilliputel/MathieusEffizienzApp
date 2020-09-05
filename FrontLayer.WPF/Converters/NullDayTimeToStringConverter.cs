using ModelLayer.Planning;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {
	public class NullDayTimeToStringConverter : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			if( value is null )
				return "The DayTime is not Overlapping!";
			else if( value is DayTime dayTime )
				return dayTime.ToString();
			else
				return value;
		}
		public object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
			throw new NotImplementedException();
		}

	}
}
