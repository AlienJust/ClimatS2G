using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter {
	public class DoubleBytesPairConverterModifyUshort : IDoubleBytesPairConverter {
		private readonly double _modifier;
		public DoubleBytesPairConverterModifyUshort(double modifier) {
			_modifier = modifier;
		}

		public BytesPair ConvertToBytesPairHighFirst(double value) {
			int integerValue = (ushort)(value/_modifier);
			byte lowByte = (byte)(integerValue & 0xFF);
			byte highByte = (byte)((integerValue & 0xFF00) >> 8);
			return new BytesPair(highByte, lowByte);
		}

		public double ConvertToSomething(BytesPair value) {
			return value.HighFirstUnsignedValue * _modifier;
		}
	}
}