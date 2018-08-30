using AlienJust.Support.Collections;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.DoubleBytesPairConverter {
	public class DoubleBytesPairConverterSimpleUshort : IDoubleBytesPairConverter {
		public BytesPair ConvertToBytesPairHighFirst(double value) {
			int integerValue = (ushort) value;
			byte lowByte = (byte)(integerValue & 0xFF);
			byte highByte = (byte)((integerValue & 0xFF00) >> 8);
			return new BytesPair(highByte, lowByte);
		}

		public double ConvertToSomething(BytesPair value) {
			return value.HighFirstUnsignedValue;
		}
	}

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

	public class DoubleBytesPairConverterModifyShort : IDoubleBytesPairConverter {
		private readonly double _modifier;
		public DoubleBytesPairConverterModifyShort(double modifier) {
			_modifier = modifier;
		}

		public BytesPair ConvertToBytesPairHighFirst(double value) {
			int integerValue = (short)(value/_modifier);
			byte lowByte = (byte)(integerValue & 0xFF);
			byte highByte = (byte)((integerValue & 0xFF00) >> 8);
			return new BytesPair(highByte, lowByte);
		}

		public double ConvertToSomething(BytesPair value) {
			return value.HighFirstUnsignedValue * _modifier;
		}
	}

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