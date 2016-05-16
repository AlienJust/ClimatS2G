using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
	public class MukFlapWorkmodeStageCommonBuilder : IBuilder<IMukFlapWorkmodeStageCommon> {
		private readonly int _absoluteValue;

		public MukFlapWorkmodeStageCommonBuilder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IMukFlapWorkmodeStageCommon Build() {
			return new MukFlapWorkmodeStageCommonSimple(_absoluteValue);
		}
	}
}