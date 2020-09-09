using System;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPFConverter {

	[ValueConversion(typeof(DataGridCell), typeof(DataRow))]
	public class DataRowViewConverter : IValueConverter {

		public object? Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			DataGridCell? cell = value as DataGridCell;
			if( cell is null )
				return null;

			System.Data.DataRowView? drv = cell.DataContext as System.Data.DataRowView;
			if( drv is null )
				return null;

			return drv.Row[cell.Column.SortMemberPath];
		}

		public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {
			throw new NotImplementedException();
		}

	}

}
