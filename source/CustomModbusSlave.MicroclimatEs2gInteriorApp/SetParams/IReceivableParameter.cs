using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface IReceivableParameter : IParameter {
		BytesPair? ReceivedUshortValue { get; set; }
		double? ReceivedDoubleValue { get; set; }
	}
}