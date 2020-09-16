using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
		[XmlAttribute("Title")]
		public string Title { get; set; }
		[XmlAttribute("Description")]
		public string Description { get; set; }

		// IChild
		[XmlElement("ParentID", IsNullable = true)]
		public Guid? ParentID {
			get {
				return _ParentID;
			}
			set {
				_ParentID = value;
				// setzt den status IsChild auf true, wenn die ID gesetzt wird
				IsChild = ( ParentID is null ) ? false : true;
			}
		}
		[XmlIgnore]
		public bool IsChild { get; set; }

		// IParent
		[XmlArray("Children")]
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent { get; set; }

		// IColorfull
		[XmlAttribute("Color")]
		public Color Color { get; set; }

		// IWorkable
		[XmlAttribute("State")]
		public EnumState State { get; set; }
		[XmlElement("Plan")]
		public DateSpan Plan { get; set; }
		[XmlAttribute("Time")]
		public TimeSpan Time { get; set; }
		[XmlArray("WorkHours")]
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
			this.Children.CollectionChanged += this.CheckIfChildrenEmpty;
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

		protected virtual void CheckIfChildrenEmpty( object sender, NotifyCollectionChangedEventArgs e ) {
			if( Children.Count <= 0 ) {
				this.IsParent = false;
				return;
			}
			this.IsParent = true;
		}

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
