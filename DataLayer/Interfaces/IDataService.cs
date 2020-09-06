using System.Collections.ObjectModel;

namespace DataLayer.Interfaces {
	public interface IDataService<T> {

		#region methods

		ObservableCollection<T> LoadData();

		void SaveData( ObservableCollection<T> Collection );

		#endregion

	}
}
