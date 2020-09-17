using LogicLayer.ViewModels;

namespace LogicLayer.Extensions {

	public delegate void BoolChangedEventHandler( bool changedBool );

	public delegate void AlertEventHanlder( string message, string buttonText, string? title, AlertSymbolEnum? symbol );

	public delegate void ViewModelChangedEventHandler( ViewModelBase newViewModel, object? passedObject );

}
