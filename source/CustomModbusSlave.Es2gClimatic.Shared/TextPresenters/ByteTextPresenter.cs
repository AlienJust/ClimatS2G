using System.Globalization;
using CustomModbusSlave.Es2gClimatic.Shared.TextPresenters.Contracts;

namespace CustomModbusSlave.Es2gClimatic.Shared.TextPresenters {
	public class ByteTextPresenter : ITextPresenter
	{
		private readonly byte _value;
		private readonly bool _showAsHex;

		public ByteTextPresenter(byte value, bool showAsHex) {
			_value = value;
			_showAsHex = showAsHex;
		}

		public string PresentAsText() {
			if (_showAsHex) return "0x" + _value.ToString("X2");
			return _value.ToString(CultureInfo.InvariantCulture);
		}
	}
}