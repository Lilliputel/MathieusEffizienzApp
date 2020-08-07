using System.Collections.ObjectModel;

namespace ModelLayer.Interfaces {

	public interface IParent<T> {

		#region properties

		public ObservableCollection<T> Children { get; set; }
		public bool IsParent { get; }

		#endregion

	}
}
