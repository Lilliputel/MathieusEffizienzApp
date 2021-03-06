﻿using EffizienzControls.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EffizienzControls.Converters {

	[ValueConversion( typeof( double ), typeof( double ) )]
	public class DivisionWithParameter : MarkedupValueConverter<DivisionWithParameter> {

		public override object? Convert( object? value, Type targetType, object parameter, CultureInfo culture ) {
			double.TryParse( (string)parameter, out double divisor );
			double dividend = (value is GridLength gl) ? gl.Value : value as double? ?? 0.0;
			double result;
			try {
				result = dividend / divisor;
			}
			catch( DivideByZeroException ) {
				throw;
			}
			return result;
		}
		public override object? ConvertBack( object? value, Type targetTypes, object parameter, CultureInfo culture ) {
			double dividend = value as double? ?? 0.0;
			double.TryParse( (string)parameter, out double divisor );
			return dividend * divisor;
		}

	}
}
