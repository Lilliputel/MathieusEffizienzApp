using EffizienzControls.Extensions;
using LogicLayer.BaseViewModels;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace FrontLayer.WPF.TemplateSelectors {
	public class ViewModelSelector : MarkedUpTemplateSelector {

		public override DataTemplate? SelectTemplate( object? item, DependencyObject container ) {
			DataTemplate? template = null;
			string vmName = item?.GetType()?.Name ?? "";
			if( item is ViewModelBase viewModel && vmName.Length > 5 ) {
				string viewName = vmName.Remove( vmName.Length - "Model".Length );
				Type? viewType = Assembly.GetExecutingAssembly()?.GetTypes().ToList().Find( t => t.Name == viewName );
				template = TemplateGenerator.CreateDataTemplate( () => CreateInstance( viewType, viewModel ) );
			}
			return template;
		}

		private object? CreateInstance( Type? viewType, ViewModelBase viewModel ) {
			FrameworkElement? obj = null;
			if( viewType is Type ) {
				obj = Activator.CreateInstance( viewType ) as FrameworkElement;
				if( obj is FrameworkElement )
					obj.DataContext = viewModel;
			}
			return obj;
		}
	}
}
