using Effizienz.Classes;
using Effizienz.Utility;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Effizienz.Views {

	public partial class ViewKategorie : UserControl {

		public ViewKategorie() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Kategorie);
		}

		~ViewKategorie() { }
	}
}
