namespace CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm {
	public class ClimaticSystemWorkModeBuilderFromInt : IBuilder<ClimaticSystemWorkMode> {
		private readonly int _integerValue;

		public ClimaticSystemWorkModeBuilderFromInt(int integerValue) {
			_integerValue = integerValue;
		}

		public ClimaticSystemWorkMode Build() {
			switch (_integerValue) {
				case 0:
					return ClimaticSystemWorkMode.Off;
				case 1:
					return ClimaticSystemWorkMode.On;
				case 2:
					return ClimaticSystemWorkMode.Reserved;
				case 3:
					return ClimaticSystemWorkMode.DowntimeWhileOn;
				case 4:
					return ClimaticSystemWorkMode.Maintenance;
				case 5:
					return ClimaticSystemWorkMode.Washing;
				case 6:
					return ClimaticSystemWorkMode.EmergencyVenting;
				case 7:
					return ClimaticSystemWorkMode.EmergencyHeating;
				case 8:
					return ClimaticSystemWorkMode.Test;
				default:
					return ClimaticSystemWorkMode.Unknown; // фигня какая-то
					//throw new Exception("Cannot convert int " + _integerValue + " to " + typeof(ClimaticSystemWorkMode).FullName);
			}
		}
	}
}