using PropertyChanged;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModelLayer.Utility {

	public class ObservableObject : INotifyPropertyChanged {

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged( [CallerMemberName] string propertyName = "" ) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		[SuppressPropertyChangedWarnings]
		protected virtual bool OnPropertyChanged<T>( ref T backingField, T value, [CallerMemberName] string propertyName = "" ) {
			if( EqualityComparer<T>.Default.Equals(backingField, value) )
				return false;
			backingField = value;
			OnPropertyChanged(propertyName);
			return true;
		}

	}
}
