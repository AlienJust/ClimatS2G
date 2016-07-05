using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface IReceivableParameter {
		BytesPair? ReceivedUshortValue { get; set; }
		double? ReceivedDoubleValue { get; set; }
	}
}