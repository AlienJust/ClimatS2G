using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data {
	internal sealed class AutomaticStageModeBuilder : IBuilder<AutomaticWorkmodeStage> {
		private readonly ushort _value;
		public AutomaticStageModeBuilder(ushort value) {
			_value = value;
		}

		public AutomaticWorkmodeStage Build() {
			switch (_value) {
				case 0:
					return AutomaticWorkmodeStage.ControllerInitialization;
				case 1:
					return AutomaticWorkmodeStage.WaitingForPowerOnCommand;
				case 2:
					return AutomaticWorkmodeStage.WorkingWithFanOnByTable;
				default:
					return AutomaticWorkmodeStage.Unknown;
			}
		}
	}
}
