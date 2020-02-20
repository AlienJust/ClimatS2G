using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel {
	internal sealed class IncomingSignalsBuilder : IBuilder<IIncomingSignals> {
		private readonly byte _data;
		public IncomingSignalsBuilder(byte data) {
			_data = data;
		}

		public IIncomingSignals Build() {
			return new IncomingSignals((_data & 0x040) == 0x40, (_data & 0x80) == 0x80);
		}
	}
}
