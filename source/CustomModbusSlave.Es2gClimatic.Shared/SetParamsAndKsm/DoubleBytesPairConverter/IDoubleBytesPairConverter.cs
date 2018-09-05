using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.BytesPairConverters.Nullable;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter {
	public interface IDoubleBytesPairConverter : IBytesPairSomethingConverter<double> {
		BytesPair ConvertToBytesPairHighFirst(double value);
	}
}
