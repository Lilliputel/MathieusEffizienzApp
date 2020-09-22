using FrontLayer.WPF.Extensions;
using ModelLayer.Classes;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object[]), typeof(double))]
	public class ConverterDateOnCategoryToDouble : MarkedupMultiValueConverter<ConverterDateOnCategoryToDouble> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			Category category = (Category)values[0];
			DateTime date = (DateTime)values[1];

			var time = category.GetTimeOnDate(date);
			return null;
		}
		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
