using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFConverter {

	public class ColorToSCB : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			Color farbe = value == null ? Colors.White : (Color)value;
			return new SolidColorBrush(farbe);
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
			return ( value as SolidColorBrush ).Color;
		}

	}
	public class StringToSCB : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			Color farbe = value == null ? Colors.White : (Color)ColorConverter.ConvertFromString(value.ToString());
			return new SolidColorBrush(farbe);
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
			return ( value as SolidColorBrush ).Color.ToString();
		}

	}
	public class StringToColor : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			Color farbe = value == null ? Colors.White : (Color)ColorConverter.ConvertFromString(value.ToString());
			return farbe;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
			return ( (Color)value ).ToString();
		}

	}

	public class DataRowViewConverter : IValueConverter {

		public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			DataGridCell cell = value as DataGridCell;
			if( cell == null )
				return null;

			System.Data.DataRowView drv = cell.DataContext as System.Data.DataRowView;
			if( drv == null )
				return null;

			return drv.Row[cell.Column.SortMemberPath];
		}

		public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			throw new NotImplementedException();
		}

	}
}
