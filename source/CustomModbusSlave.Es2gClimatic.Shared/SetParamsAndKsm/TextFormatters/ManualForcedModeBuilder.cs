namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	public class ManualForcedModeBuilder : IBuilder<ManualForcedMode> {
		private readonly ushort _value;
		public ManualForcedModeBuilder(ushort value) {
			_value = value;
		}

		public ManualForcedMode Build() {
			switch (_value) {
				case 1:
					return ManualForcedMode.Cool100Percent;
				case 2:
					return ManualForcedMode.Cool50Percent;
				case 3:
					return ManualForcedMode.Fan;
				case 4:
					return ManualForcedMode.Heat100Percent;
				case 5:
					return ManualForcedMode.Heat50Percent;
				default:
					return ManualForcedMode.Unknown;
			}
		}
	}
}