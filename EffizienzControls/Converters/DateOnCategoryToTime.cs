using EffizienzControls.Extensions;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion(typeof(object[]), typeof(TimeSpan))]
	public class DateOnCategoryToTime : MarkedupMultiValueConverter<DateOnCategoryToTime> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			Category category = (Category)values[0];
			DateTime date = (DateTime)values[1];
			#endregion

			return ( category as IAccountableParent<Goal> ).GetTimeOnDate(date);
		}
		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
