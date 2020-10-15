using EffizienzControls.Extensions;
using ModelLayer.Classes;
using ModelLayer.Interfaces;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion(typeof(object[]), typeof(double))]
	public class DateOnCategoryToHeight : MarkedupMultiValueConverter<DateOnCategoryToHeight> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			var category = (Category)values[0];
			var date = (DateTime)values[1];
			var maxTime = (TimeSpan)values[2];
			var totalHeight = (double)values[3];
			double factor = 0.0;
			#endregion

			#region conversion
			var workedTime = (category as IAccountableParent<Goal>).GetTimeOnDate(date);

			if( maxTime > TimeSpan.Zero )
				factor = workedTime / maxTime;
			#endregion

			return factor * totalHeight;
		}
		public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
