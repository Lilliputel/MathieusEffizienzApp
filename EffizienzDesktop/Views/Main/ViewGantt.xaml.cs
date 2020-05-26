using Effizienz.Interfaces;
using System;
using System.Windows.Controls;

namespace Effizienz.Views {

	public partial class ViewGantt : UserControl, IParsable {

		public ViewGantt() {
			InitializeComponent();
		}

		~ViewGantt() { }

		public bool Parse() => throw new NotImplementedException();
		public void Wipe() => throw new NotImplementedException();

	}
}
