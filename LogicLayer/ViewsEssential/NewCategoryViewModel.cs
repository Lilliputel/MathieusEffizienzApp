using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Enums;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewCategoryViewModel : ViewModelBase {

		#region fields

		private string? _Title;
		private string? _Description;
		private EnumState _State = EnumState.ToDo;
		private (Color color, string name)? _SelectedColor;

		private ICommand? _SaveCategoryCommand;

		#endregion

		#region properties

		public string? Title {
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
		public string? Description {
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
				return this._State;
			}
			set {
				if( value == _State )
					return;
				_State = value;
				OnPropertyChanged(nameof(State));
			}
		}

#warning Cannot convert Windows.Media.Color to Drawing.Color
		public IEnumerable<(Color color, string name)> ColorList
			=> from property in typeof(System.Windows.Media.Colors).GetProperties() orderby property.GetValue(null, null)?.ToString() select ((Color)property.GetValue(null, null)!, property.Name);

		public (Color color, string name)? SelectedColor {
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

		public ICommand SaveCategoryCommand => _SaveCategoryCommand ??=
			new RelayCommand(parameter => {
				if( Title is string && SelectedColor is (Color color, string name) item && Description is string ) {
					ObjectManager.CategoryList.Add(
						new Category(
							Title,
							item.color,
							Description,
							State)
						);
					MessageBoxManager.ObjektErstellt(nameof(Category), Title);
				}
				else {
					MessageBoxManager.InputInkorrekt("");
				}
			});

		#endregion

		#region constructor

		#endregion

		#region methods

		#endregion

	}
}
