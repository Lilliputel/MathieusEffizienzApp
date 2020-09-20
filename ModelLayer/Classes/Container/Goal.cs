using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Utility;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	public class Goal : ObservableObject, IUnique, IChild, IWorkable, IParent<Goal> {

		#region fields

		private Guid? _ParentID;

		#endregion

		#region Properties

		// IUnique
		[XmlIgnore]
		public Guid ID { get; }
		[XmlAttribute(nameof(Title))]
		public string Title { get; set; } = "New Goal";
		[XmlAttribute(nameof(Description))]
		public string Description { get; set; } = "This is a Goal without a specific description!";

		// IChild
		[XmlElement(nameof(ParentID), IsNullable = true)]
		[AlsoNotifyFor(nameof(IsChild))]
		public Guid? ParentID { get; set; }
		[XmlIgnore]
		public bool IsChild
			=> ParentID is Guid;

		// IParent
		[XmlArray(nameof(Children))]
		[AlsoNotifyFor(nameof(IsParent))]
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent
			=> Children.Count > 0;

		// IColorfull
		[XmlAttribute(nameof(Color))]
		public Color Color { get; set; }

		// IWorkable
		[XmlAttribute(nameof(State))]
		public EnumState State { get; set; }
		[XmlElement(nameof(Plan))]
		public DateSpan Plan { get; set; }
		[XmlAttribute(nameof(Time))]
		public TimeSpan Time { get; set; }
		[XmlArray(nameof(WorkHours))]
		public ObservableCollection<(DateTime Date, TimeSpan Time)> WorkHours { get; set; }


		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Goal must have a Titel and a ParentID
		/// </summary>
		public Goal() {
			this.ID = Guid.NewGuid();
			this.Time = TimeSpan.Zero;
			this.WorkHours = new ObservableCollection<(DateTime Date, TimeSpan Time)>();
			this.Plan = new DateSpan(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2));

			// initialize Children-Collection and add a Eventhandler
			this.Children = new ObservableCollection<Goal>();
		}

		public Goal( string title, DateSpan plan, string description = "New Goal!", EnumState state = EnumState.ToDo )
			: this() {
			this.Title = title;
			this.Plan = plan;
			this.Description = description;
			this.State = state;
		}

		~Goal() { }

		#endregion

		#region Methods

		public Goal? GetChild( Guid ID ) {
			if( ID == this.ID ) {
				return this;
			}
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
