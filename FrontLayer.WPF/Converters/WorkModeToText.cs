using EffizienzControls.Extensions;
using ModelLayer.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converters {

	[ValueConversion( typeof( WorkModeEnum ), typeof( string ) )]
	public class WorkModeToText : MarkedupValueConverter<WorkModeToText> {
		public override object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
			var mode = (WorkModeEnum) value;
			var button = (string) parameter;
			string retVal;

			if( button == "StartStop" )
				retVal = mode == WorkModeEnum.Stop ? "Start timer!" : "Stop timer!";
			else
				retVal = mode switch
				{
					WorkModeEnum.Stop => "Start work!",
					WorkModeEnum.Work => "Delay break!",
					WorkModeEnum.Break => "Delay work!",
					WorkModeEnum.DelayWork => "Back to work!",
					WorkModeEnum.DelayBreak => "Take a break!",
					_ => "Error!"
				};

			return retVal;
		}
		public override object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
