using CustomModbusSlave.MicroclimatEs2gApp.TextPresenters.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.TextPresenters {
	public class DataDoubleTextPresenter : ITextPresenter {
		private readonly byte _lowByte;
		private readonly byte _highByte;
		private readonly double _modifier;
		private readonly int _digitsAfterDotCount;

		public DataDoubleTextPresenter(byte lowByte, byte highByte, double modifier, int digitsAfterDotCount)
		{
			_lowByte = lowByte;
			_highByte = highByte;
			_modifier = modifier;
			_digitsAfterDotCount = digitsAfterDotCount;
		}

		public string PresentAsText() {
			if (_highByte == 0x85 && _lowByte == 0x00) return "Обрыв датчика";
			return ((_highByte*256.0 + _lowByte)*_modifier).ToString("f" + _digitsAfterDotCount);
		}
	}
}