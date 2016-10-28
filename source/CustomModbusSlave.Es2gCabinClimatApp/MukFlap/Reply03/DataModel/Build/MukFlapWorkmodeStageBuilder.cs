using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Build {
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