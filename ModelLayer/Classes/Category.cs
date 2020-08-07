using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.Utility;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	public class Category : ObservableObject, IIdentifyable, IParent<Goal>, IColorfull, IStatus {

		#region fields

		private string title;
		private string description;

		private bool isParent;

		private Color color;
		private EnumStatus status;

		#endregion

		#region Properties

		// IIdentifyable
		[XmlIgnore]
		public Guid ID {
			get;
		}
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

		// IParent
		public ObservableCollection<Goal> Children { get; set; }
		[XmlIgnore]
		public bool IsParent {
			get {
				return isParent;
			}
			set {
				isParent = value;
				OnPropertyChanged(nameof(IsParent));
			}
		}

		// IStatus
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

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor for serialisation!
		/// instance of Category must have a Titel and a Color!
		/// </summary>
		public Category() {
			this.ID = Guid.NewGuid();
			this.Status = EnumStatus.ToDo;

			// initialize Children-Collection and add a Eventhandler
			this.Children = new ObservableCollection<Goal>();
			this.Children.CollectionChanged += this.CheckIfChildrenEmpty;
		}

		public Category( string _Titel, Color _Farbe ) : this() {
			this.Title = _Titel;
			this.Color = _Farbe;
		}

		~Category() { }

		#endregion

		#region Methods

		protected virtual void CheckIfChildrenEmpty( object sender, NotifyCollectionChangedEventArgs e ) {
			if( Children == new ObservableCollection<Goal>() ) {
				this.IsParent = false;
				return;
			}
			this.IsParent = true;
		}

		#endregion

	}
}
