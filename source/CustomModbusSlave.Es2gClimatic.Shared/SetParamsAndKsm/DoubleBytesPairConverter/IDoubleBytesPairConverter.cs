using AlienJust.Support.Collections;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter {
	public interface IDoubleBytesPairConverter : IBytesPairSomethingConverter<double> {
		BytesPair ConvertToBytesPairHighFirst(double value);
	}
}