using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Gantt_Diagramm {
    public class EventStartConverter : IMultiValueConverter {

        public object Convert( object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture ) {

            TimeSpan timelineDuration = (TimeSpan)values[0];
            TimeSpan startTime = (TimeSpan)values[1];

            double containerWidth = (double)values[2];
            double factor = startTime.TotalSeconds / timelineDuration.TotalSeconds;

            double startValue = factor * containerWidth;

            return new Thickness(startValue, 0, 0, 0);
        }

        public object[] ConvertBack( object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
