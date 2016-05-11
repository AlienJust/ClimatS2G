namespace CustomModbusSlave.MicroclimatEs2gApp.ProgamLog {
	class LogLineSimple : ILogLine {
		private readonly string _messageText;
		public LogLineSimple(string messageText) {
			_messageText = messageText;
		}

		public string MessageText {
			get {
				return _messageText;
			}
		}
	}
}
