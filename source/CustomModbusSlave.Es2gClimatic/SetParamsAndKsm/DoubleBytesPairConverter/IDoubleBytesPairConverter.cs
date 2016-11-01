using AlienJust.Support.Collections;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.DoubleBytesPairConverter {
	public interface IDoubleBytesPairConverter : IBytesPairSomethingConverter<double> {
		BytesPair ConvertToBytesPairHighFirst(double value);
	}
}