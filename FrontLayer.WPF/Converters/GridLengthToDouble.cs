using FrontLayer.WPF.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion(typeof(GridLength), typeof(Double))]
	public class GridLengthToDouble : MarkedupValueConverter<GridLengthToDouble> {

		public override object Convert( object value, Type targetType, object parameter, CultureInfo culture )
			=> (double)( (GridLength)value ).Value;

		public override object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> new GridLength((double)value);

	}
}
