using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.Utility;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Media;

namespace LogicLayer.Views {
	public class NewCategoryViewModel : ViewModelBase {

		#region fields

		private string? _Title;
		private string? _Description;
		private EnumState _State = EnumState.ToDo;
		private PropertyInfo? _SelectedColor;

		private ICommand? _SaveCategoryCommand;

		#endregion

		#region properties

		public string Title {
			get {
				return _Title;
			}
			set {
				if( value == _Title )
					return;
				_Title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		public string Description {
			get {
				return _Description;
			}
			set {
				if( value == _Description )
					return;
				_Description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		public EnumState State {
			get {
				return (EnumState)this._State;
			}
			set {
				if( value == _State )
					return;
				_State = value;
				OnPropertyChanged(nameof(State));
			}
		}

		public IEnumerable<PropertyInfo> ColorList
			=> from property in typeof(Colors).GetProperties() orderby property.GetValue(null, null)?.ToString() select property;
		public PropertyInfo SelectedColor {
			get {
				return _SelectedColor;
			}
			set {
				if( value == _SelectedColor )
					return;
				_SelectedColor = value;
				OnPropertyChanged(nameof(SelectedColor));
			}
		}

		public ICommand SaveCategoryCommand => _SaveCategoryCommand
			??= new RelayCommand(parameter => {
				ObjectManager.CategoryList.Add(
					new Category(
						Title,
						(Color)SelectedColor.GetValue(null, null)!,
						Description,
						State)
					);
				MessageBoxDisplayer.ObjektErstellt(nameof(Category), Title);
			});

		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion

	}
}
