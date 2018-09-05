using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.SimpleRelease;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Build {
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
