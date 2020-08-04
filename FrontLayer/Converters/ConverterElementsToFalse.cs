using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FrontLayer.Converters {
	public class ConverterElementsToFalse : IMultiValueConverter {

        public object Convert( object[] values, Type targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
            bool titel = (string)values[0] != String.Empty;
            bool isSelectedComboBox = (bool)values[1];
            if( values.Length > 2 ) {
                bool isSelectedEndDatum = (bool)values[2];
                bool isSelectedStartDatum = (bool)values[3];
                return titel && isSelectedComboBox && isSelectedStartDatum && isSelectedEndDatum;
            }
            return titel && isSelectedComboBox;
        }

		public object[] ConvertBack( object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
            throw new ArgumentException();
        }
    
    }
}
