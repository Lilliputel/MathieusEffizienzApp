using System.Xml.Serialization;

namespace ModelLayer.Structs {

	public struct Time {

		[XmlAttribute]
		public int Hour { get; set; }
		[XmlAttribute]
		public int Minute { get; set; }

		public Time( int _Hour, int _Minute ) {
			this.Hour = _Hour;
			this.Minute = _Minute;
		}

	}

}
