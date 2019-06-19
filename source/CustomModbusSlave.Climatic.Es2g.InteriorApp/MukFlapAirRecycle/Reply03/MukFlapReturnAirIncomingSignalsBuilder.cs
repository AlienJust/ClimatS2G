using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	internal sealed class MukFlapReturnAirIncomingSignalsBuilder : IBuilder<IMukFlapReturnAirIncomingSignals> {
		private readonly byte _data;
		public MukFlapReturnAirIncomingSignalsBuilder(byte data) {
			_data = data;
		}

		public IMukFlapReturnAirIncomingSignals Build() {
			return new MukFlapReturnAirIncomingSignals(!_data.GetBit(0), !_data.GetBit(1),!_data.GetBit(2)); // bits are inverted
		}
	}
}
