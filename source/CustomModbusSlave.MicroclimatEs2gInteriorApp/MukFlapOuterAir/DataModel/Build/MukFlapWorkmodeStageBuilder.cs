using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.SimpleRelease;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
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