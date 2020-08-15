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
	public class NewCategoryViewModel : ViewModelBase {

		#region fields

		private ICommand? _SaveCategoryCommand;

		#endregion

		#region properties

		public IEnumerable<PropertyInfo> ColorList
			=> from property in typeof(Colors).GetProperties() orderby property.GetValue(null, null)?.ToString() select property;


		public PropertyInfo? SelectedColor { get; set; }

		public string? Title { get; set; }

		public ICommand SaveCategoryCommand => _SaveCategoryCommand
			??= new RelayCommand(parameter => {
				ObjectManager.CategoryList.Add(
					new Category(
						Title ??= "New_Category",
						(Color)SelectedColor?.GetValue(null, null)!));
				MessageBoxDisplayer.ObjektErstellt(nameof(Category), Title);
			});

		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion

	}
}
