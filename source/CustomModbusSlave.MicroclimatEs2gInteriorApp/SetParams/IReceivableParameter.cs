using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface IReceivableParameter: IParameter {
		BytesPair? ReceivedBytesValue { set; }
	}
}