namespace LogicLayer.BaseViewModels {
	public abstract class ContentViewModelBase<T> : ViewModelBase, IContent<T> {
		public abstract bool Clear();
		public abstract bool Fill( T item );
	}
}
