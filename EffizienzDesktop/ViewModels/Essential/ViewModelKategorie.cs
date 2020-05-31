using Effizienz.Classes;
using Effizienz.Commands;
using Effizienz.Utility;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Effizienz.Views {
	public class ViewModelKategorie : ViewModelBase {

		#region fields

		private ICommand commandSaveKategorie;

		#endregion

		#region properties

		public IEnumerable<PropertyInfo> FarbenListe
			=> from property in typeof(Colors).GetProperties() orderby property.GetValue(null, null).ToString() select property;


		public PropertyInfo SelectedColor { get; set;}

		public string Titel { get; set; }

		public ICommand CommandSaveKategorie => commandSaveKategorie ??
			( commandSaveKategorie = new CommandRelay(parameter => {
				( Application.Current as App ).KategorienListe.Add(
					new Kategorie(						
						Titel, 
						(Color)SelectedColor.GetValue(null, null)));
				MessageBoxDisplayer.ObjektErstellt(nameof(Kategorie), Titel);
			}) );
		#endregion

		#region constructor

		#endregion

	}
}
