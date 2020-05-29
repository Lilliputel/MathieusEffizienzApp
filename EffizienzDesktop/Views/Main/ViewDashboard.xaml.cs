using Effizienz.Classes;
using Effizienz.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewDashboard : UserControl {

		public ViewDashboard() {
			InitializeComponent();

			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Dashboard);

		}

		~ViewDashboard() { }

		private void Button_Kategorien_Speichern_Click( object sender, RoutedEventArgs e ) => 
			( Application.Current as App ).Speichern();

	}
}
