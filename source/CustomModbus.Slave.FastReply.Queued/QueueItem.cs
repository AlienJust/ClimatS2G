using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm {
	public class QueueItem {
		public int ParameterIndex { get; set; }
		public ushort ParameterValue { get; set; }
		public Action<Exception> OnCompleteCallback { get; set; }
		public int AttemptsCount { get; set; }
	}
}