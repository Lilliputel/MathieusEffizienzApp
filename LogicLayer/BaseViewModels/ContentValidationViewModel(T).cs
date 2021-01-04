namespace LogicLayer.BaseViewModels {
	public abstract class ContentValidationViewModel<T> : ValidationViewModel, IContent<T> {
		public abstract bool Clear();
		public abstract bool Fill( T item );
	}
}
