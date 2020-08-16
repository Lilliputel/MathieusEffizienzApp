using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Planning;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class PlanViewModel : ViewModelBase {

		#region fields

		#endregion

		#region properties

		public ObservableCollection<Category> Categories
			=> ObjectManager.CategoryList;

		public WeekPlan WeekPlan
			=> ObjectManager.WeekPlan;


		#endregion

		#region constructor

		public PlanViewModel() {
			WeekPlan.PropertyChanged += Test;
		}


		#endregion

		#region methods

		private void Test( object sender, System.ComponentModel.PropertyChangedEventArgs e ) {
			Debug.WriteLine($"Executed PropertyChanged {e.PropertyName}");
		}

		#endregion

	}
}
