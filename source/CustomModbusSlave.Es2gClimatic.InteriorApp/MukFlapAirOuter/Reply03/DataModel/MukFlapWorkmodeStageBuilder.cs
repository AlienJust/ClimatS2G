using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel {
	class MukFlapWorkmodeStageBuilder : IBuilder<IMukFlapWorkmodeStage> {
		private readonly int _absoluteValue;

		public MukFlapWorkmodeStageBuilder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapWorkmodeStage Build() {
			return new MukFlapWorkmodeStageSimple(_absoluteValue);

		}
	}
}
