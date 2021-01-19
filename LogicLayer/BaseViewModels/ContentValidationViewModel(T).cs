namespace LogicLayer.BaseViewModels {
	public abstract class ContentValidationViewModel<T> : ValidationViewModel, IContent<T> {
		public abstract void Clear();
		public abstract void Fill( T item );
	}
}
