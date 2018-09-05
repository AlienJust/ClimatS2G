using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.SetParams.BytesPairNullableConverters {
	public interface IBytesPairSomethingConverter<out T> {
		T ConvertToSomething(BytesPair value);
	}
}
