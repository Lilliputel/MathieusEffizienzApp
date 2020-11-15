using EffizienzControls.Extensions;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FrontLayer.WPF.Converter {

	[ValueConversion( typeof( ViewModelEnum ), typeof( ViewModelBase ) )]
	public class EnumToViewModel : MarkedupValueConverter<EnumToViewModel> {
		public override object? Convert( object value, Type targetType, object parameter, CultureInfo culture )
			=> value is ViewModelEnum vM ? ViewModelManager.GetViewModel( vM ) : null;
		public override object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> throw new NotImplementedException();
	}
}
