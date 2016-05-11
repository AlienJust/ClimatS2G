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
}