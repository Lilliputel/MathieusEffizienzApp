using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EffizienzControls.Converters {
	[ValueConversion( typeof( double[] ), typeof( Thickness ) )]
	public class DoublesToOffset : MarkedupMultiValueConverter<DoublesToOffset> {
		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
			=> new Thickness(
				(values[0] is double d1 && double.IsNaN( d1 ) is false ? d1 : 0.0) * (values[1] is double d2 && double.IsNaN( d2 ) is false ? d2 : 0.0),
				0, 0, 0 );

	}
}
