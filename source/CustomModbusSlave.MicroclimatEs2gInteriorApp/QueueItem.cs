using System;

namespace CustomModbusSlave.MicroclimatEs2gApp {
	class QueueItem {
		public int ParameterIndex { get; set; }
		public ushort ParameterValue { get; set; }
		public Action<Exception> OnCompleteCallback { get; set; }
		public int AttemptsCount { get; set; }
	}
}