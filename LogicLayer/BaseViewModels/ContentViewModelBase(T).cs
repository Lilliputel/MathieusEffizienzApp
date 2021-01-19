namespace LogicLayer.BaseViewModels {
	public abstract class ContentViewModelBase<T> : ViewModelBase, IContent<T> {
		public abstract void Clear();
		public abstract void Fill( T item );
	}
}
