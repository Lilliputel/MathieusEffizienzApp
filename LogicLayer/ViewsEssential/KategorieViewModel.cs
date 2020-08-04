using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.Utility;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media;

namespace LogicLayer.Views {
	public class KategorieViewModel : ViewModelBase {

		#region fields

		private ICommand commandSaveKategorie;

		#endregion

		#region properties

		public IEnumerable<PropertyInfo> FarbenListe
			=> from property in typeof(Colors).GetProperties() orderby property.GetValue(null, null).ToString() select property;


		public PropertyInfo SelectedColor { get; set; }

		public string Titel { get; set; }

		public ICommand CommandSaveKategorie => commandSaveKategorie ??
			( commandSaveKategorie = new CommandRelay(parameter => {
				ObjectManager.KategorienListe.Add(
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
