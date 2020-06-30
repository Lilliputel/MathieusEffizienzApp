using UiLayer.Classes;
using UiLayer.Utility;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UiLayer.Views {

	public partial class ViewProjekt : UserControl {

		public ViewProjekt() {
			InitializeComponent();
			this.DataContext = NameContainer.GetViewModel(EnumViewModels.Projekt);
		}
	}
}
