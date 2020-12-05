using System.IO;

namespace DataLayer {

	public interface IErrorHandler {

		#region events

		event ErrorEventHandler ErrorOccured;

		#endregion

	}
}
