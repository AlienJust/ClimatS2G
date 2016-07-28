using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
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