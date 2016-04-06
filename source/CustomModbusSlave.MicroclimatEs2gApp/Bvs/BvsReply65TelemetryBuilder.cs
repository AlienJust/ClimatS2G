using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.Bvs {

	internal class BvsReply65TelemetryBuilder : IBuilder<IBvsReply65Telemetry> {
		private readonly IList<byte> _data;

		public BvsReply65TelemetryBuilder(IList<byte> data) {
			_data = data;
		}

		public IBvsReply65Telemetry Build() {

		}
	}
}