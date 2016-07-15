using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	public interface ISettableBytesPairtParameter : IParameter {
		BytesPair? BytesValue { get; }
	}
}