using System.Globalization;
using CustomModbusSlave.Es2gClimatic.Shared.TextPresenters.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.TextPresenters {
	public class UshortTextPresenter : ITextPresenter
	{
		private readonly byte _lowByte;
		private readonly byte _highByte;
		private readonly bool _showAsHex;

		public UshortTextPresenter(byte lowByte, byte highByte, bool showAsHex)
		{
			_lowByte = lowByte;
			_highByte = highByte;
			_showAsHex = showAsHex;
		}

		public string PresentAsText() {
			if (_showAsHex) return "0x" + _highByte.ToString("X2") + _lowByte.ToString("X2");
			return (_highByte*256 + _lowByte).ToString(CultureInfo.InvariantCulture);
		}
	}
}
