using Effizienz.Interfaces;
using System;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewPlanung : UserControl, IParsable {

		public ViewPlanung() {
			InitializeComponent();
		}

		~ViewPlanung() { }

		public bool Parse() => throw new NotImplementedException();
		public void Wipe() => throw new NotImplementedException();

	}
}
