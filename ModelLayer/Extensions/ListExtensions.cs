using System.Collections.Generic;

namespace ModelLayer.Extensions {

	public static class ListExtensions {

		public static ICollection<T>? AddUnique<T>( this ICollection<T>? extension, T item ) {
			if( extension is ICollection<T> && extension.Contains( item ) is false )
				extension.Add( item );
			return extension;
		}

		public static ICollection<T>? AddUniqueRange<T>( this ICollection<T>? extension, ICollection<T>? collection ) {
			if( extension is ICollection<T> && collection is ICollection<T> )
				foreach( T item in collection )
					extension.AddUnique( item );
			return extension;
		}

		public static ICollection<T>? AddRange<T>( this ICollection<T>? extension, ICollection<T>? collection ) {
			if( extension is ICollection<T> && collection is ICollection<T> )
				foreach( T item in collection )
					extension.Add( item );
			return extension;
		}

	}
}
