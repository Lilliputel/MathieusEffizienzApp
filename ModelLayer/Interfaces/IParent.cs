using ModelLayer.Classes;

namespace ModelLayer.Interfaces {
	public interface IParent<T> where T : class {
		Children<T> Children { get; }
	}
}
