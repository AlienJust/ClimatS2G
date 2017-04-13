namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public class TextFormatterIntegerDotted : ITextFormatter<int> {

		public string Format(int value) {
			var frmt = value.ToString("D");
			var result = string.Empty;
			
			if (frmt == string.Empty) return result;

			result += frmt[0];
			for (int i = 1; i < frmt.Length; ++i) {
				result += "." + frmt[i];
			}
			return result;
		}
	}
}