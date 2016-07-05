using AlienJust.Support.Collections;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class DoubleBytesPairConverterSimpleUshort : IDoubleBytesPairConverter {
		public BytesPair ConvertToBytesPairHighFirst(double value) {
			int integerValue = (ushort) value;
			byte lowByte = (byte)(integerValue & 0xFF);
			byte highByte = (byte)((integerValue & 0xFF00) >> 8);
			return new BytesPair(highByte, lowByte);
		}

		public double ConvertToDoubleHighFirst(BytesPair value) {
			return value.HighFirstUnsignedValue;
		}
	}

	class DoubleBytesPairConverterSimpleShort : IDoubleBytesPairConverter {
		public BytesPair ConvertToBytesPairHighFirst(double value) {
			int integerValue = (short)value;
			byte lowByte = (byte)(integerValue & 0xFF);
			byte highByte = (byte)((integerValue & 0xFF00) >> 8);
			return new BytesPair(highByte, lowByte);
		}

		public double ConvertToDoubleHighFirst(BytesPair value) {
			return value.HighFirstUnsignedValue;
		}
	}
}