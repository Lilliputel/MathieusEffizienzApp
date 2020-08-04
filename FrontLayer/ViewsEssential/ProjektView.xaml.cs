﻿using System.Windows.Controls;
using FrontLayer.Utility;

namespace FrontLayer.Views {

	public partial class ProjektView : UserControl {

		public ProjektView() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Projekt);
		}
	}
}