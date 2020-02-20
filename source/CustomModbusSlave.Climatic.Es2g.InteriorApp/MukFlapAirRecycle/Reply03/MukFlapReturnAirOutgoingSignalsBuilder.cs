using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	internal sealed class MukFlapReturnAirOutgoingSignalsBuilder : IBuilder<IMukFlapReturnAirOutgoingSignals> {
		private readonly byte _data;
		public MukFlapReturnAirOutgoingSignalsBuilder(byte data) {
			_data = data;
		}

		public IMukFlapReturnAirOutgoingSignals Build() {
			return new MukFlapReturnAirOutgoingSignals(_data.GetBit(0), _data.GetBit(1));
		}
	}
}
