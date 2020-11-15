using ModelLayer.Enums;
using System;

namespace ModelLayer.Attributes {

	[AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false )]
	public class ViewAttribute : Attribute {
		public ViewAttribute( ViewModelEnum type, bool isView ) {
			Type = type;
			IsView = isView;
		}

		public ViewModelEnum Type { get; set; }
		public bool IsView { get; set; }


	}
}
