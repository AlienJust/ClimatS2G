namespace CustomModbusSlave.Es2gClimatic.Shared.MukCondenser.Request16 {
	internal class MukCondenserWorkmodeCommandFromKsmSimple : IMukCondenserWorkmodeCommandFromKsm {
		public bool RegulatorIsWorking { get; set; }
		public bool RegulatorIsWorkingParking { get; set; }
		public bool Washing { get; set; }
		public bool FanSpeedIsMax { get; set; }
		public bool FanIsOff { get; set; }
		public bool ManualMode { get; set; }
		public bool ForceTurnOnSecondStageOnly { get; set; }
	}
}
