using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Structs;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	public class Goal : ObservableObject, IUnique, IParent<Goal>, IChild, IWorkable {

		#region fields

		private string title;
		private string description;

		private Guid parentID;
		private bool isChild;

		private bool isParent;

		private Color color;

		private EnumStatus status;
		private DateSpan plan;
		private TimeSpan time;

		#endregion

		#region Properties

		// IUnique
		[XmlIgnore]
		public Guid ID { get; }
		[XmlAttribute("Title")]
		public string Title {
			get {
				return title;
			}
			set {
				title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		[XmlAttribute("Description")]
		public string Description {
			get {
				return description;
			}
			set {
				description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		// IChild
		[XmlAttribute("ParentID")]
		public Guid ParentID {
			get {
				return parentID;
			}
			set {
				parentID = value;
				OnPropertyChanged(nameof(ParentID));

				// setzt den status IsChild auf true, wenn die ID gesetzt wird
				IsChild = ParentID == null ? false : true;
			}
		}
		[XmlIgnore]
		public bool IsChild {
			get {
				return isChild;
			}
			private set {
				isChild = value;
				OnPropertyChanged(nameof(IsChild));
			}
		}

		// IParent
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent {
			get {
				return isParent;
			}
			private set {
				isParent = value;
				OnPropertyChanged(nameof(IsParent));
			}
		}

		// IColorfull
		[XmlElement]
		public Color Color {
			get {
				return color;
			}
			set {
				color = value;
				OnPropertyChanged(nameof(Color));
			}
		}

		// IWorkable
		[XmlAttribute("Status")]
		public EnumStatus Status {
			get {
				return status;
			}
			set {
				status = value;
				OnPropertyChanged(nameof(Status));
			}
		}
		[XmlElement]
		public DateSpan Plan {
			get {
				return plan;
			}
			set {
				plan = value;
				OnPropertyChanged(nameof(Plan));
			}
		}
		[XmlAttribute("Time")]
		public TimeSpan Time {
			get {
				return time;
			}
			set {
				time = value;
				OnPropertyChanged(nameof(Time));
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Goal must have a Titel and a ParentID
		/// </summary>
		public Goal() {
			this.ID = Guid.NewGuid();
			this.Time = TimeSpan.Zero;

			// initialize Children-Collection and add a Eventhandler
			this.Children = new ObservableCollection<Goal>();
			this.Children.CollectionChanged += this.CheckIfChildrenEmpty;
		}

		public Goal( string _Title, DateTime _StartDate, DateTime _EndDate, EnumStatus _Status = EnumStatus.ToDo, string _Description = "New Goal!" )
			: this() {
			this.Title = _Title;
			this.Plan = new DateSpan(_StartDate, _EndDate);
			this.Status = _Status;
			this.Description = _Description;
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

		public Goal GetChild( Guid ID ) {
			if( ID == this.ID ) {
				return this;
			}
			Goal placeholder;
			foreach( Goal child in this.Children ) {
				placeholder = child.GetChild(ID);
				if( placeholder != null && ID == placeholder.ID ) {
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
