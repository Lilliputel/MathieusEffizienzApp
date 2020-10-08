using ModelLayer.Utility;
using System;
using System.Xml.Serialization;

namespace ModelLayer.Classes {

	[Serializable]
	public class Category : ObservableObject {

		#region Properties
		[XmlElement(nameof(ID))]
		public Identification ID { get; set; }
		[XmlArray(nameof(Children))]
		public Children Children { get; set; }
		[XmlArray(nameof(WorkSessions))]
		public CategoryPlan WorkSessions { get; set; }
		#endregion

		#region Constructors
		public Category( Identification id ) : this() {
			this.ID = id;
		}
		public Category() {
			this.Children = new Children(this.ID);
			this.WorkSessions = new CategoryPlan();
		}
		~Category() { }
		#endregion

		#region Methods
		public TimeSpan GetTimeOnDate( DateTime date ) {
			var placeholder = new TimeSpan();
			foreach( var goal in Children )
				placeholder += goal.WorkHours.GetTimeOnDate(date.Date);
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
