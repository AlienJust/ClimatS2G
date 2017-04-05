using AlienJust.Support.Collections;
using ParamCentric.Common.Contracts;

namespace ParamCentric.Modbus.Contracts {
	public interface ISettableBytesPairtParameter : IParameter {
		BytesPair? BytesValue { get; }
	}
}