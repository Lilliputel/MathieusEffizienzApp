using System.Collections.ObjectModel;

namespace ModelLayer.Interfaces {
	public interface IParent<T> where T : class {

		public bool IsParent
			=> Children.Count > 0;
		ObservableCollection<T> Children { get; }
	}
}
