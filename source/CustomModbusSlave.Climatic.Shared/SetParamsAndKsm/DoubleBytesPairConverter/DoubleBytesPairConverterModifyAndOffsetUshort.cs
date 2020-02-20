using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter {
	/// <summary>
	/// Converts bytes pair to double and back using multiplier and linear offset
	/// </summary>
	public class DoubleBytesPairConverterModifyAndOffsetUshort : IDoubleBytesPairConverter {
		private readonly double _modifier;
		private readonly double _offset;

		public DoubleBytesPairConverterModifyAndOffsetUshort(double modifier, double offset) {
			_modifier = modifier;
			_offset = offset;
		}

		public double ConvertToSomething(BytesPair value) {
			return value.HighFirstUnsignedValue * _modifier + _offset;
		}

		public BytesPair ConvertToBytesPairHighFirst(double value) {
			int integerValue = (ushort)((value - _offset) / _modifier);
			byte lowByte = (byte)(integerValue & 0xFF);
			byte highByte = (byte)((integerValue & 0xFF00) >> 8);
			return new BytesPair(highByte, lowByte);
		}
	}
}