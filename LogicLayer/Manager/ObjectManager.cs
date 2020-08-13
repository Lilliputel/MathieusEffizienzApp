using ModelLayer.Classes;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LogicLayer.Manager {
	public static class ObjectManager {

		#region properties

		/// <summary>
		/// Die Globale Categories, welche gespeichert wird und alle Categories enthalten Sollte.
		/// </summary>
		public static ObservableCollection<Category> CategoryList { get; set; }

		private static int counter = 0;

		#endregion

		#region constructor
		static ObjectManager() {

			CategoryList = new ObservableCollection<Category>();

		}

		#endregion

		#region methods

		public static void GenerateObjects() {
			Random randomGen = new Random();
			Color randomColor =
				Color.FromArgb(
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255),
				(byte)randomGen.Next(255));

			// new Category
			Category CBCategory1 = new Category("CodeBehind-NewCategory", randomColor);

			Task.Run(() => CBCategory1.WeekPlan.AddTimeAsync((DayOfWeek)randomGen.Next(7), new DayTime(TimeSpan.FromHours(0 + counter), TimeSpan.FromHours(1 + counter))));

			Goal CBGoal1 = new Goal("CodeBehind-NewGoal1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(8)){
				Time = new TimeSpan(1, 2, 3)
			};
			Goal CBGoal1_1 = new Goal("CodeBehind-NewGoal1.1", DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)){
				Time = new TimeSpan(3, 12, 20)
			};
			Goal CBGoal2 = new Goal("CodeBehind-NewGoal2", DateTime.Today, DateTime.Today.AddDays(10)){
				Time = new TimeSpan(10, 30, 0)
			};

			CBGoal1.AddChild(CBGoal1_1);

			CBCategory1.AddChild(CBGoal1);
			CBCategory1.AddChild(CBGoal2);

			CategoryList.Add(CBCategory1);


			counter++;
		}

		#endregion
	}
}
