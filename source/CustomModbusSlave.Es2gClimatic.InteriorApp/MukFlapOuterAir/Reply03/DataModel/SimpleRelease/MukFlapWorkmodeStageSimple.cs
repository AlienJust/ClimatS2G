using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts.Enums;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.SimpleRelease {
	class MukFlapWorkmodeStageSimple : IMukFlapWorkmodeStage {
		public MukFlapWorkmodeStageSimple(int absoluteValue) {
			AbsoluteValue = absoluteValue;
		}

		public int AbsoluteValue { get; }

		public MukFlapOuterAirWorkmodeStage KnownValue
		{
			get
			{
				switch (AbsoluteValue) {
					case 0:
						return MukFlapOuterAirWorkmodeStage.ControllerInitialization;
					case 1:
						return MukFlapOuterAirWorkmodeStage.FlapTesting;
					case 2:
						return MukFlapOuterAirWorkmodeStage.WorkMode;
					case 3:
						return MukFlapOuterAirWorkmodeStage.WorkModeWithFailedFlap;
					case 4:
						return MukFlapOuterAirWorkmodeStage.WorkModePwmCorrectionBack;
					case 5:
						return MukFlapOuterAirWorkmodeStage.NoKsm;
					default:
						return MukFlapOuterAirWorkmodeStage.Unknown;
				}
			}
		}
	}
}