using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter {
	public class DoubleBytesPairConverterSimpleShort : IDoubleBytesPairConverter {
		public BytesPair ConvertToBytesPairHighFirst(double value) {
			int integerValue = (short)value;
			byte lowByte = (byte)(integerValue & 0xFF);
			byte highByte = (byte)((integerValue & 0xFF00) >> 8);
			return new BytesPair(highByte, lowByte);
		}

		public double ConvertToSomething(BytesPair value) {
			return value.HighFirstUnsignedValue;
		}
	}
}