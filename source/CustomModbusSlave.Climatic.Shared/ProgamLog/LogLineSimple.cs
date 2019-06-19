namespace CustomModbusSlave.Es2gClimatic.Shared.ProgamLog {
	public class LogLineSimple : ILogLine {
		public LogLineSimple(string messageText) {
			MessageText = messageText;
		}

		public string MessageText { get; }
	}
}
