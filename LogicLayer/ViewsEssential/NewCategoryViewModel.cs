using LogicLayer.Commands;
using LogicLayer.Manager;
using LogicLayer.ViewModels;
using ModelLayer.Classes;
using ModelLayer.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Input;

namespace LogicLayer.Views {
	public class NewCategoryViewModel : ViewModelBase {

		#region private fields
		private ObservableCollection<Category> CategoryList;
		private string? _Title;
		private string? _Description;
		private StateEnum _State = StateEnum.ToDo;
		private string? _SelectedColorName;

		private ICommand? _SaveCategoryCommand;
		#endregion

		#region public properties
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
		public StateEnum State {
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
		public List<string> ColorNameList {
			get {
				var allColors = new List<string>();
				PropertyInfo[] propertyInfos = typeof(System.Drawing.Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public );
				foreach( PropertyInfo propertyInfo in propertyInfos ) {
					allColors.Add(propertyInfo.Name);
					//if( propertyInfo.GetValue(null, null) is Color color )
					//	allColors.Add((color, propertyInfo.Name));
				}
				return allColors;
			}
		}
		public string? SelectedColorName {
			get {
				return _SelectedColorName;
			}
			set {
				if( value == _SelectedColorName )
					return;
				_SelectedColorName = value;
				OnPropertyChanged(nameof(SelectedColorName));
			}
		}
		#endregion

		#region public commands
		public ICommand SaveCategoryCommand => _SaveCategoryCommand ??=
			new RelayCommand(parameter => {
				if( Title is string && SelectedColorName is string && Description is string ) {
					CategoryList.Add(
						new Category(
							new Identification(
								Title,
								Description,
								Color.FromName(SelectedColorName))));
					AlertManager.ObjektErstellt(nameof(Category), Title);
				}
				else {
					AlertManager.InputInkorrekt("");
				}
			});

		#endregion

		#region constructor
		public NewCategoryViewModel( ObservableCollection<Category> categories ) {
			this.CategoryList = categories;
		}
		#endregion

		#region methods

		#endregion

	}
}
