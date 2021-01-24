using EffizienzControls.Extensions;
using ModelLayer.Classes;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( object[] ), typeof( TimeSpan ) )]
	public class CategoryToTime : MarkedupMultiValueConverter<CategoryToTime> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			if( values[0] is Category cat )
				if( values[1] is DateTime dt )
					return cat.GetTotalTimeOnDate( dt );
				else
					throw new ArgumentException( "The second value must be a DateTime", nameof( values ) );
			else
				throw new ArgumentException( "The first value must be a Category!", nameof( values ) );
		}

	}
}
