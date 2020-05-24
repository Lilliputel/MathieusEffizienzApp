using Effizienz.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Effizienz.Classes {
	public class ListeIIdentifizierbar<T> : INotifyPropertyChanged where T : IIdentifizierbar {

		public ObservableCollection<T> Liste { get; private set; } = new ObservableCollection<T>();

		public T IdToObject( int _ID ) {
			return (from item in Liste 
					where item.ID == _ID 
					select item)
					.First();
		}

		public void AddMember( T inputObjekt ) {
			if( !Liste.Contains(inputObjekt) ) {
				Liste.Add(inputObjekt);
				OnPropertyChanged(nameof(Liste));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged( string propertyName ) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
