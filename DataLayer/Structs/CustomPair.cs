using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataLayer.Structs {

	[Serializable]
	public struct CustomPair<TKey, TValue> {

		[XmlAttribute("Key")]
		public TKey Key { get; set; }
		[XmlAttribute("Value")]
		public TValue Value { get; set; }

		public CustomPair( KeyValuePair<TKey, TValue> keyValuePair )
			: this(keyValuePair.Key, keyValuePair.Value) { }

		public CustomPair( TKey key, TValue value ) {
			this.Key = key;
			this.Value = value;
		}

	}
}
