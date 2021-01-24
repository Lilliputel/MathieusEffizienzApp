using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( double[] ), typeof( GridLength ) )]
	public class DoublesToGridLenght : MarkedupMultiValueConverter<DoublesToGridLenght> {

		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {

			#region input
			double subtrahend = (double)values[0];
			double minuend = (double)values[1];
			#endregion

			#region converstion
			double returnValue = subtrahend - minuend < 0 ? 0.0 : subtrahend - minuend;
			#endregion

			return new GridLength( returnValue );

		}

	}
}
