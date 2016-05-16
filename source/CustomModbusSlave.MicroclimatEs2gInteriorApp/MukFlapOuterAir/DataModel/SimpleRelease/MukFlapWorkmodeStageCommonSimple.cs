using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	public class MukFlapWorkmodeStageSimple : IMukFlapWorkmodeStage {
		public MukFlapWorkmodeStageSimple(int absoluteValue) {
			AbsoluteValue = absoluteValue;
		}

		public int AbsoluteValue { get; }

		public MukFlapWorkmodeStage KnownValue
		{
			get
			{
				switch (AbsoluteValue) {
					case 0:
						return MukFlapWorkmodeStage.ControllerInitialization;
					case 1:
						return MukFlapWorkmodeStage.FlapTesting;
					case 2:
						return MukFlapWorkmodeStage.WorkMode;
					case 3:
						return MukFlapWorkmodeStage.WorkModeWithFailedFlap;
					case 4:
						return MukFlapWorkmodeStage.WorkModePwmCorrectionBack;
					case 5:
						return MukFlapWorkmodeStage.NoKsm;
					default:
						return MukFlapWorkmodeStage.Unknown;
				}
			}
		}
	}
}