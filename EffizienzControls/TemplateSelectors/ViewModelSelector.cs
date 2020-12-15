using EffizienzControls.Extensions;
using ModelLayer.Attributes;
using ModelLayer.Enums;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace EffizienzControls.TemplateSelectors {
	public class ViewModelSelector : MarkedUpTemplateSelector {

		public override DataTemplate? SelectTemplate( object item, DependencyObject container ) {
			DataTemplate? template = null;
			if( item is ViewModelEnum viewModelEnum ) {
				Type type = Assembly.GetEntryAssembly()!.GetTypes()
					.Where( type => type.IsDefined( typeof( ViewAttribute ) ) )
					.Where( type => (type.GetCustomAttribute( typeof( ViewAttribute ) ) is ViewAttribute attr) && attr.Type == viewModelEnum && attr.IsView is true )
					.First();
				template = TemplateGenerator.CreateDataTemplate( () => Activator.CreateInstance( type ) );
			}
			return template;
		}
	}
}
