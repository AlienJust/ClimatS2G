using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams {
	interface IReceivableParameter<out T> : IParameter {
		BytesPair? ReceivedBytesValue { set; }
		T ReceivedValueFormatted { get; }
	}


	interface ISettableByUserParameter<T> : IParameter {
		T FormattedValue { get; set; }
	}

	interface ISettableBytesPairtParameter : IParameter {
		BytesPair? BytesValue { get; }
	}
}