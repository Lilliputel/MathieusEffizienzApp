using FrontLayer.WPF.Extensions;
using LogicLayer.Manager;
using ModelLayer.Classes;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(object[]), typeof(double))]
	public class DateOnCategoryToHeight : MarkedupMultiValueConverter<DateOnCategoryToHeight> {

#warning somehow i have to get the totalheight

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			var category = (Category)values[0];
			var date = (DateTime)values[1];
			var maxTime = (TimeSpan)values[2];

			var totalHeight = 350;//((GridLength)values[2]).Value - 30;

			var workedTime = category.GetTimeOnDate(date);

			double factor = 0.0;
			if( maxTime > TimeSpan.Zero )
				factor = workedTime / maxTime;

			return factor * totalHeight;
		}
		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
