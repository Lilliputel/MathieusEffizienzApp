using FrontLayer.WPF.Extensions;
using ModelLayer.Planning;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(DoubleTime), typeof(string))]
	public class ConverterNullDayTimeToString : MarkedupValueConverter<ConverterNullDayTimeToString> {

		public override object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			if( value is null )
				return "The DayTime is not Overlapping!";
			else if( value is DoubleTime dayTime )
				return dayTime.ToString();
			else {
				Debug.WriteLine($"Tried to Convert a different Object[{value}] in ConverterNullToString");
				return value;
			}
		}

		public override object? ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();

	}
}
