using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	class DoubleTextFormatterBits : IDoubleTextFormatter {
		private readonly string _nullFormat;

		public DoubleTextFormatterBits(string nullFormat) {
			_nullFormat = nullFormat;
		}

		public string Format(double? value) {
			if (!value.HasValue) return _nullFormat;
			return Convert((ushort)value.Value);
		}

		string Convert(int x) {
			char[] bits = new char[32];
			int i = 0;

			while (x != 0) {
				bits[i++] = (x & 1) == 1 ? '1' : '0';
				x >>= 1;
			}

			Array.Reverse(bits, 0, i);
			return new string(bits);
		}
	}
}