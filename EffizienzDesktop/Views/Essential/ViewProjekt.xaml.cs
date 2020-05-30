using Effizienz.Classes;
using Effizienz.Utility;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewProjekt : UserControl {

		public ViewProjekt() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Projekt);
		}
	}
}
