using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace LogicLayer.Manager {
	public static class ObjectManager {

		#region properties

		/// <summary>
		/// Die Globale Categories, welche gespeichert wird und alle Categories enthalten Sollte.
		/// </summary>
		public static ObservableCollection<Category> CategoryList { get; set; }

		#endregion

		#region constructor
		static ObjectManager() {

			CategoryList = new ObservableCollection<Category>();

		}

		#endregion

		#region methods

		public static void GenerateObjects() {
			Category CBCategory1 = new Category("CodeBehind-NewCategory", Colors.DeepSkyBlue);
			Goal CBGoal1 = new Goal("CodeBehind-Projekt", CBCategory1.ID, DateTime.Today.AddDays(1), DateTime.Today.AddDays(8)){
				Time = new TimeSpan(1, 2, 3)
			};
			Goal CBGoal1_1 = new Goal("CodeBehind-NewGoal", CBCategory1.ID, DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)){
				Time = new TimeSpan(3, 12, 20)
			};
			Goal CBGoal2 = new Goal("CodeBehind-Projekt", CBCategory1.ID, DateTime.Today, DateTime.Today.AddDays(10)){
				Time = new TimeSpan(10, 30, 0)
			};

			CBGoal1.Children.Add(CBGoal1_1);

			CBCategory1.Children.Add(CBGoal1);
			CBCategory1.Children.Add(CBGoal2);

			CategoryList.Add(CBCategory1);
		}

		#endregion
	}
}
