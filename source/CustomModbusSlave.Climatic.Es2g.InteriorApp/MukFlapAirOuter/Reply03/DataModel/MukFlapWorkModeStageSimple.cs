using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel {
	internal sealed class MukFlapWorkModeStageSimple : IMukFlapWorkmodeStage {
		public MukFlapWorkModeStageSimple(int absoluteValue) {
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
					case 6:
						return MukFlapOuterAirWorkmodeStage.SetFromRemoteController;
					case 7:
						return MukFlapOuterAirWorkmodeStage.FlapCloseInWashingMode;
					default:
						return MukFlapOuterAirWorkmodeStage.Unknown;
				}
			}
		}
	}
}
