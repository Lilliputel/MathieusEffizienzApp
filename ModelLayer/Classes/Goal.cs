using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	public class Goal : ObservableObject, IUnique, IChild, IWorkable, IParent<Goal> {

		#region fields

		private string? _Title;
		private string? _Description;

		private Guid? _ParentID;
		private bool _IsChild;

		private bool _IsParent;

		private string _ColorHex;

		private EnumState _State = EnumState.ToDo;
		private DateSpan _Plan;
		private TimeSpan _Time;

		#endregion

		#region Properties

		// IUnique
		[XmlIgnore]
		public Guid ID { get; }
		[XmlAttribute("Title")]
		public string Title {
			get {
				return _Title ??= "New_Goal!";
			}
			set {
				_Title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		[XmlAttribute("Description")]
		public string Description {
			get {
				return _Description ??= "This is a new standardGoal!";
			}
			set {
				_Description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		// IChild
		[XmlElement("ParentID", IsNullable = true)]
		public Guid? ParentID {
			get {
				return _ParentID;
			}
			set {
				_ParentID = (Guid?)value;
				OnPropertyChanged(nameof(ParentID));

				// setzt den status IsChild auf true, wenn die ID gesetzt wird
				IsChild = ( ParentID is null ) ? false : true;
			}
		}
		[XmlIgnore]
		public bool IsChild {
			get {
				return _IsChild;
			}
			private set {
				_IsChild = value;
				OnPropertyChanged(nameof(IsChild));
			}
		}

		// IParent
		[XmlArray("Children")]
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent {
			get {
				return _IsParent;
			}
			private set {
				_IsParent = value;
				OnPropertyChanged(nameof(IsParent));
			}
		}

		// IColorfull
		[XmlAttribute("Color")]
		public string ColorHex {
			get {
				return _ColorHex;
			}
			set {
				_ColorHex = value;
				OnPropertyChanged(nameof(ColorHex));
			}
		}

		// IWorkable
		[XmlAttribute("State")]
		public EnumState State {
			get {
				return _State;
			}
			set {
				_State = value;
				OnPropertyChanged(nameof(State));
			}
		}
		[XmlElement("Plan")]
		public DateSpan Plan {
			get {
				return _Plan;
			}
			set {
				_Plan = value;
				OnPropertyChanged(nameof(Plan));
			}
		}
		[XmlAttribute("Time")]
		public TimeSpan Time {
			get {
				return _Time;
			}
			set {
				_Time = value;
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
			this._Time = TimeSpan.Zero;
			this._Plan = new DateSpan(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2));

			// initialize Children-Collection and add a Eventhandler
			this.Children = new ObservableCollection<Goal>();
			this.Children.CollectionChanged += this.CheckIfChildrenEmpty;
		}

		public Goal( string title, DateSpan plan, string description = "New Goal!", EnumState state = EnumState.ToDo )
			: this() {
			this._Title = title;
			this._Plan = plan;
			this._Description = description;
			this._State = state;
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
			_Child.ColorHex = this.ColorHex;

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
