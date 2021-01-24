using EffizienzControls.Extensions;
using ModelLayer.Classes;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( object[] ), typeof( double ) )]
	public class CategoryToFactor : MarkedupMultiValueConverter<CategoryToFactor> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			if( values[0] is Category cat )
				if( values[1] is DateTime dt )
					if( values[2] is TimeSpan maxTime )
						return maxTime > TimeSpan.Zero
							? cat.GetTotalTimeOnDate( dt ) / maxTime
							: 0.0;
					else
						throw new ArgumentException( "The third value must be a TimeSpan", nameof( values ) );
				else
					throw new ArgumentException( "The second value must be a DateTime", nameof( values ) );
			else
				throw new ArgumentException( "The first value must be a Category!", nameof( values ) );
		}

	}
}
