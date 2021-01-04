using LogicLayer.BaseViewModels;

namespace LogicLayer.Extensions {

	public delegate void AlertEventHanlder( string message, string buttonText, string? title, AlertSymbolEnum? symbol );
	public delegate void ViewModelChangedEventHandler( ViewModelBase newViewModel, object? passedObject );

}
