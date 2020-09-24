using FrontLayer.WPF.Extensions;
using ModelLayer.Classes;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object[]), typeof(double))]
	public class DateOnCategoryToHeight : MarkedupMultiValueConverter<DateOnCategoryToHeight> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			var one = ( (Category)values[0] ).GetTimeOnDate((DateTime)values[1]);

			return null;
		}
		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
