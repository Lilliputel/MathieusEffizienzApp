using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Planning;
using ModelLayer.Utility;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	public class Category : ObservableObject, IUnique, IStatus, IParent<Goal>, IAccountable {

		#region Properties

		// IUnique
		[XmlIgnore]
		public Guid ID { get; }
		[XmlAttribute(nameof(Title))]
		public string Title { get; set; } = "New Category";
		[XmlAttribute(nameof(Description))]
		public string Description { get; set; } = "This is a Category without description!";

		// IParent
		[XmlArray(nameof(Children))]
		[AlsoNotifyFor(nameof(IsParent))]
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent
			=> Children.Count > 0;

		// WeekPlan
		[XmlArray(nameof(WorkSessions))]
		public ObservableCollection<(DayOfWeek Day, DoubleTime Time)> WorkSessions { get; set; }
		public event NotifyCollectionChangedEventHandler? WeekPlanChanged;

		// IStatus
		[XmlAttribute(nameof(State))]
		public EnumState State { get; set; }

		// IColorfull
		[XmlElement(nameof(Color))]
		public Color Color { get; set; }

		public TimeSpan Time {
			get {
				var bridge = new TimeSpan();
				foreach( var child in Children )
					bridge += child.GetTotalTime();
				return bridge;
			}
		}


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

			// initialize WeekPlan
			this.WorkSessions = new ObservableCollection<(DayOfWeek Day, DoubleTime Time)>();
			this.WorkSessions.CollectionChanged += WorkTimes_CollectionChanged;
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

		public TimeSpan GetTimeOnDate( DateTime date ) {
			var placeholder = new TimeSpan();
			foreach( var goal in Children )
				placeholder += goal.GetTimeOnDate(date.Date);
			return placeholder;
		}
		public TimeSpan GetTotalTimeOnDate( DateTime date ) {
			var placeholder = new TimeSpan();
			foreach( var goal in Children )
				placeholder += goal.GetTotalTimeOnDate(date.Date);
			return placeholder;
		}

		#endregion

	}
}
