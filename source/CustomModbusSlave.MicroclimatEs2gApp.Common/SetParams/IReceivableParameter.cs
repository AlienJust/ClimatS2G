using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public interface IReceivableParameter : IParameter {
		BytesPair? ReceivedBytesValue { set; }
	}
}