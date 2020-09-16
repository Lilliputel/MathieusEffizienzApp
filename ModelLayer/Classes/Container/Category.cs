using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Planning;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	public class Category : ObservableObject, IUnique, IStatus, IParent<Goal> {

		#region Properties

		// IUnique
		[XmlIgnore]
		public Guid ID { get; }
		[XmlAttribute("Title")]
		public string Title { get; set; }
		[XmlAttribute("Description")]
		public string Description { get; set; }

		// IParent
		[XmlArray("Children")]
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent { get; set; }

		// WeekPlan
		[XmlArray("WorkTimes")]
		public ObservableCollection<(DayOfWeek Day, DoubleTime Time)> WorkTimes { get; set; }
		public event EventHandler<NotifyCollectionChangedEventArgs>? WeekPlanChanged;

		// IStatus
		[XmlAttribute("Status")]
		public EnumState State { get; set; }

		// IColorfull
		[XmlAttribute("Color")]
		public Color Color { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Category must have a Titel and a Color!
		/// </summary>
		public Category() {
			this.ID = Guid.NewGuid();
			this.State = EnumState.ToDo;

			// initialize Children-Collection and add a Eventhandler
			this.Children = new ObservableCollection<Goal>();
			this.Children.CollectionChanged += this.CheckIfChildrenEmpty;

			// initialize WeekPlan
			this.WorkTimes = new ObservableCollection<(DayOfWeek Day, DoubleTime Time)>();
			this.WorkTimes.CollectionChanged += WorkTimes_CollectionChanged;
		}

		public Category( string title, Color color, string description = "New Category", EnumState state = EnumState.ToDo ) : this() {
			this.Title = title;
			this.Description = description;
			this.Color = color;
			this.State = state;
		}

		~Category() { }

		#endregion

		#region Methods

		private void WorkTimes_CollectionChanged( object sender, NotifyCollectionChangedEventArgs e ) => WeekPlanChanged?.Invoke(this, e);

		protected virtual void CheckIfChildrenEmpty( object sender, NotifyCollectionChangedEventArgs e ) {
			if( Children.Count <= 0 ) {
				this.IsParent = false;
				return;
			}
			this.IsParent = true;
		}

		public Goal? GetChild( Guid ID ) {
			Goal? placeholder;
			foreach( Goal child in this.Children ) {
				placeholder = child.GetChild(ID);
				if( placeholder is { ID: Guid phID } && ID == phID ) {
					return placeholder;
				}
			}
			return null;
		}
		public void AddChild( Goal _Child ) {
			_Child.ParentID = this.ID;
			_Child.Color = this.Color;

			this.Children.Add(_Child);
		}
		public void AddChildren( Collection<Goal> _Children ) {
			foreach( Goal child in _Children ) {
				AddChild(child);
			}
		}

		#endregion

	}
}
