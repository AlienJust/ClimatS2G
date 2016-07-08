using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface ISettableBytesPairtParameter : IParameter {
		BytesPair? BytesValue { get; }
	}
}