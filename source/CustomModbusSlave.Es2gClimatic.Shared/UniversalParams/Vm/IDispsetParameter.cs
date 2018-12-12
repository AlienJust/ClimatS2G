using System.ComponentModel;
using System.Windows.Input;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.Vm {
	public interface IDispsetParameter<TDispset> : IDisplayParameter, INotifyPropertyChanged {
		TDispset DispsetValue { get; set; }
		bool IsDispsetValueFallbackOrUnknown { get; }
		ICommand SetCommand { get; }
	}
}