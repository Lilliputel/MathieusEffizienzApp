namespace DataLayer.MockDataService {
	public class MockDataService {

		//public ObservableCollection<Category> LoadData() {
		//	var zwischenSpeicher = new ObservableCollection<TCategory>();

		//	for( int i = 0; i <= 3; i++ ) {
		//		Random randomGen = new Random();
		//		Color randomColor =
		//			Color.FromArgb(
		//			(byte)randomGen.Next(255),
		//			(byte)randomGen.Next(255),
		//			(byte)randomGen.Next(255),
		//			(byte)randomGen.Next(255));

		//		// new Category
		//		TCategory CBCategory1 = new TCategory($"Generated-Category{i}", randomColor);

		//		TGoal CBGoal1 = new Goal($"Generated-Goal{counter}_1", new DateSpan( DateTime.Today.AddDays(1), DateTime.Today.AddDays(8)) ){
		//			Time = new TimeSpan(1, 2, 3)
		//		};
		//		TGoal CBGoal1_1 = new Goal($"Generated-Goal{counter}_1.1", new DateSpan( DateTime.Today.AddDays(2),  DateTime.Today.AddDays(5)) ){
		//			Time = new TimeSpan(3, 12, 20)
		//		};
		//		TGoal CBGoal2 = new Goal($"Generated-Goal{counter}_2", new DateSpan( DateTime.Today, DateTime.Today.AddDays(10)) ){
		//			Time = new TimeSpan(10, 30, 0)
		//		};

		//		CBGoal1.AddChild(CBGoal1_1);

		//		CBCategory1.AddChild(CBGoal1);
		//		CBCategory1.AddChild(CBGoal2);
		//		CBCategory1.WorkTimes.Add(((DayOfWeek)randomGen.Next(7), new DayTime((0.0 + counter, 1.0 + counter))));

		//		zwischenSpeicher.Add(CBCategory1);
		//	}

		//}

		//public void SaveData( ObservableCollection<TCategory> Collection ) {
		//	Debug.WriteLine("Tried to save the Collection in the MockDataService");
		//}

	}
}
