using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	class MukFlapReturnAirConcentratorStatusBuilderFromByte : IBuilder<IMukFlapReturnAirConcentratorStatus> {
		private readonly byte _b;

		public MukFlapReturnAirConcentratorStatusBuilderFromByte(byte b) {
			_b = b;
		}

		public IMukFlapReturnAirConcentratorStatus Build() {
			return new MukFlapReturnAirConcentratorStatusSimple {
				CommandToPal = _b.GetBit(0),
				CommandFromPal = _b.GetBit(1),
				WorkOrError = _b.GetBit(2),
				ErrorNoAnswerFromDriver = _b.GetBit(3),
				ErrorByCurrentCc = _b.GetBit(4),
				ComparatorValue = _b.GetBit(5),
				Reserve = _b.GetBit(6),
				Address = _b.GetBit(7)
			};
		}
	}
}