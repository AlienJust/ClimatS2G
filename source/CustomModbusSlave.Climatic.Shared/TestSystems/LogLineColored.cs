using System;
using AlienJust.Adaptation.WindowsPresentation.Converters;
using CustomModbusSlave.Es2gClimatic.Shared.ProgamLog;

namespace CustomModbusSlave.Es2gClimatic.Shared.TestSystems {
	public class LogLineColored : ILogLine, IEquatable<LogLineColored> {
		public string MessageText { get; }
		public Colors BackgroundColor { get; }

		public LogLineColored(string messageText, Colors backgroundColor) {
			BackgroundColor = backgroundColor;
			MessageText = messageText;
		}

		public bool Equals(LogLineColored other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(MessageText, other.MessageText) && BackgroundColor == other.BackgroundColor;
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((LogLineColored) obj);
		}

		public override int GetHashCode() {
			unchecked {
				return ((MessageText != null ? MessageText.GetHashCode() : 0) * 397) ^ (int) BackgroundColor;
			}
		}
	}
}
