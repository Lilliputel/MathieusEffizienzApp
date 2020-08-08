using System;
using System.Collections.ObjectModel;

namespace ModelLayer.Interfaces {

	public interface IParent<T> {

		#region properties

		public ObservableCollection<T> Children { get; set; }
		public bool IsParent { get; }

		#endregion

		#region methods

		public T GetChild( Guid _ID );

		public void AddChild( T _Child );

		public void AddChildren( Collection<T> _Children );

		#endregion

	}
}
