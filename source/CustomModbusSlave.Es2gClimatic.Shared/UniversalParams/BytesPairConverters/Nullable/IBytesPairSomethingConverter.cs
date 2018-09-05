using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.UniversalParams.BytesPairConverters.Nullable {
	public interface IBytesPairSomethingConverter<out T> {
		T ConvertToSomething(BytesPair value);
	}
}
