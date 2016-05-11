using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
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

	class OneWireSensorErrorCodeSimple : IOneWireSensorErrorCode {
		public OneWireSensorErrorCodeSimple(int absoluteValue) {
			AbsoluteValue = absoluteValue;
		}

		public int AbsoluteValue { get; }

		public OneWireSensorErrorCode KnownValue {
			get {
				switch (AbsoluteValue) {
					case 0x0A:
						return OneWireSensorErrorCode.FoundDeviceWithUnknownFamilyCode;
					case 0x0C:
						return OneWireSensorErrorCode.SensorNotFound;
					case 0x0B:
						return OneWireSensorErrorCode.NoReactionOnReset;
					case 3:
						return OneWireSensorErrorCode.SensorShowsIncorrectWorking;
					default:
						return OneWireSensorErrorCode.NoError;
				}
			}
		}
	}
	
}