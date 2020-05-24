using EffizienzNeu.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EffizienzNeu.Classes {
	public class MenuItem : ObservableObject {

		private Projekt projektItem;
		public Projekt ProjektItem {
			get {
				return projektItem;
			}
			set {
				projektItem = value;
				OnPropertyChanged(nameof(ProjektItem));
			}
		}

		public ObservableCollection<Aufgabe> Aufgaben { get; set; }

		public MenuItem( Projekt _projekt) {
			this.ProjektItem = _projekt;
			this.Aufgaben = new ObservableCollection<Aufgabe>();
			foreach( Aufgabe item in ListContainer.AufgabenListe.Liste ) {
				if( item.ProjektID == projektItem.ID ) {
					Aufgaben.Add(item);
				}
			}
		}

	}
}
