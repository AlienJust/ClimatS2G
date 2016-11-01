namespace CustomModbusSlave.MicroclimatEs2gApp.Common.ProgamLog {
	public class LogLineSimple : ILogLine {
		public LogLineSimple(string messageText) {
			MessageText = messageText;
		}

		public string MessageText { get; }
	}
}
