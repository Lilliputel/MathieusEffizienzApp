using Effizienz.Interfaces;
using System;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewStatistik : UserControl, IParsable {

		public ViewStatistik() {
			InitializeComponent();
		}

		~ViewStatistik() { }

		public bool Parse() => throw new NotImplementedException();
		public void Wipe() => throw new NotImplementedException();

	}
}
