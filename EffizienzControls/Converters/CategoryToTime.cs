using EffizienzControls.Extensions;
using ModelLayer.Classes;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( object[] ), typeof( TimeSpan ) )]
	public class CategoryToTime : MarkedupMultiValueConverter<CategoryToTime> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			var category = (Category)values[0];
			var date = (DateTime)values[1];
			#endregion

			return (category).GetTotalTimeOnDate( date );
		}

	}
}
