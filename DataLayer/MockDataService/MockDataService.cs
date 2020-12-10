using ModelLayer.Classes;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace DataLayer {
	public class MockDataService : IRepository {

		#region IRepository
		public ObservableCollection<Category> LoadAll()
			=> GenerateData();
		public T GetById<T>( int id ) where T : class {
			Debug.WriteLine( $"Tried to load item with id {nameof( id )}[{id}] in the MockDataService" );
			return default( T );
		}
		public bool Update<T>( T item ) where T : class {
			Debug.WriteLine( $"Tried to update {nameof( item )}[{item}] in the MockDataService" );
			return false;
		}
		#endregion

		#region private helpermethods
		private ObservableCollection<Category> GenerateData() {
			var placeholder = new ObservableCollection<Category>();
			for( int counter = 0; counter <= 3; counter++ ) {
				var randomGen = new Random();
				var randomColor = Color.FromArgb( 255,
					(byte)randomGen.Next( 255 ),
					(byte)randomGen.Next( 255 ),
					(byte)randomGen.Next( 255 ) );

				var CodeCat = new Category( new UserText( $"Generated-Category{counter}", null, randomColor ) ) {
					Children = {

						new Goal(new UserText($"Generated-Goal{counter}_1", null, randomColor), new DateSpan(DateTime.Today.AddDays(1), DateTime.Today.AddDays(8))) {
							Children = {
								new Goal( new UserText($"Generated-Goal{counter}_1.1", null, randomColor), new DateSpan(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5))){
									WorkHours = {
										new WorkItem(DateTime.Today.AddDays(randomGen.Next(-10, 11)), TimeSpan.FromHours(randomGen.NextDouble() * 10), TimeSpan.Zero, TimeSpan.Zero)
									}
								}

							}
						},
						new Goal(new UserText($"Generated-Goal{counter}_2", null, randomColor), new DateSpan(DateTime.Today, DateTime.Today.AddDays(10)))
					}
				};
				CodeCat.WorkPlan.Add( new DoubleTime( (DayOfWeek)randomGen.Next( 7 ), 0.0 + counter, 1.0 + counter, CodeCat ) );

				placeholder.Add( CodeCat );
			}
			return placeholder;
		}

		public bool Insert<T>( T item ) where T : class
			=> throw new NotImplementedException();
		public bool Delete<T>( int id ) where T : class {
			Debug.WriteLine( $"Tried to delete {nameof( id )}[{id}] in the MockDataService" );
			return false;
		}
		public bool Save() {
			Debug.WriteLine( $"Tried to save in the MockDataService" );
			return false;
		}

		public bool Delete<T>( T item ) where T : class {
			Debug.WriteLine( $"Tried to delete {nameof( item )}[{item}] in the MockDataService" );
			return false;
		}
		#endregion
	}
}
