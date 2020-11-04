﻿using System.Collections.Generic;

namespace ModelLayer.Extensions {

	public static class ListExtensions {

		public static ICollection<T> AddUnique<T>( this ICollection<T> extension, T Item ) {
			if( extension.Contains( Item ) is false )
				extension.Add( Item );
			return extension;
		}

		public static ICollection<T> AddUniqueRange<T>( this ICollection<T> extension, ICollection<T> Collection ) {
			foreach( T item in Collection )
				extension.AddUnique( item );
			return extension;
		}

		public static ICollection<T> AddRange<T>( this ICollection<T> extension, ICollection<T> Collection ) {
			foreach( T item in Collection )
				extension.Add( item );
			return extension;
		}

	}
}
