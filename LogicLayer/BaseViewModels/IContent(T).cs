namespace LogicLayer.BaseViewModels {
	public interface IContent<T> {
		void Fill( T item );
		void Clear();
	}
}
