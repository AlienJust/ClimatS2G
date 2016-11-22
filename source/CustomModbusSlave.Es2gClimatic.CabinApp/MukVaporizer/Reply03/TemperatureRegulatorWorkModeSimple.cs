using CustomModbusSlave.Es2gClimatic.CabinApp.MukVaporizer.Reply03;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukVaporizer {
	class TemperatureRegulatorWorkModeSimple : ITemperatureRegulatorWorkMode {
		public TemperatureRegulatorWorkModeSimple(int fullValue, bool cool, bool restrict) {
			FullValue = fullValue;
			Cool = cool;
			Restrict = restrict;
		}

		public int FullValue { get; }
		public bool Cool { get; }
		public bool Restrict { get; }
	}
}