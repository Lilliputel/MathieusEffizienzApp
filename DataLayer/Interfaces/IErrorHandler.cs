using System;
using System.IO;

namespace DataLayer.Interfaces {

	public interface IErrorHandler {

		#region events

		event ErrorEventHandler ErrorOccured;

		#endregion

	}
}
