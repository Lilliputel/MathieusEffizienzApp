using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Effizienz.Converters {
	public class ConverterElementsToFalse : IMultiValueConverter {

        public object Convert( object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
            bool titel = (string)values[0] != String.Empty;
            bool isSelectedComboBox = (bool)values[1];
            bool isSelectedEndDatum = (bool)values[2];
            return titel && isSelectedComboBox && isSelectedEndDatum;
        }

		public object[] ConvertBack( object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
            throw new ArgumentException();
        }
    
    }
}
