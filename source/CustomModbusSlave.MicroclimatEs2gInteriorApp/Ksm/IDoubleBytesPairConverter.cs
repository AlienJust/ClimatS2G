using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface IDoubleBytesPairConverter {
		BytesPair ConvertToBytesPairHighFirst(double value);
		double ConvertToDoubleHighFirst(BytesPair value);
	}
}