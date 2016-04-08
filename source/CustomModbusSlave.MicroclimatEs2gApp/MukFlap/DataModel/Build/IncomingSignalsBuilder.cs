using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
	class IncomingSignalsBuilder : IBuilder<IIncomingSignals> {
		private readonly byte _data;
		public IncomingSignalsBuilder(byte data) {
			_data = data;
		}

		public IIncomingSignals Build() {
			return new IncomingSignals((_data & 0x040) == 0x40, (_data & 0x80) == 0x80);
		}
	}

	class MukFlapDiagnosticOneWireSensorBuilder : IBuilder<IMukFlapDiagnosticOneWireSensor> {
		private readonly int _data;
		public MukFlapDiagnosticOneWireSensorBuilder(int data) {
			_data = data;
		}

		public IMukFlapDiagnosticOneWireSensor Build() {
			return new MukFlapDiagnosticOneWireSensor(
				(_data & 0x80) == 0x80, // zbbit 7
				new OneWireSensorErrorCodeSimple(_data & 0xFF7F), // error code
				(_data & 0xFF7F) < 5 ? (int?)(_data & 0xFF7F) : null // errors count
				);
		}
	}
}