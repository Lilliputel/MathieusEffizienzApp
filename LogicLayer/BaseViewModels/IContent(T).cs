namespace LogicLayer.BaseViewModels {
	public interface IContent<T> {
		bool Fill( T item );
		bool Clear();
	}
}
