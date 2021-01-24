﻿using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EffizienzControls.Converters {
	[ValueConversion( typeof( double[] ), typeof( Thickness ) )]
	public class DoublesToOffset : MarkedupMultiValueConverter<DoublesToOffset> {
		public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
			#region input
			double Factor1 = (double)values[0];
			double Factor2 = (double)values[1];
			double offset = Factor1 is double.NaN || Factor2 is double.NaN ? 0.0 : Factor1 * Factor2;
			#endregion
			return new Thickness( offset, 0, 0, 0 );
		}

	}
}
