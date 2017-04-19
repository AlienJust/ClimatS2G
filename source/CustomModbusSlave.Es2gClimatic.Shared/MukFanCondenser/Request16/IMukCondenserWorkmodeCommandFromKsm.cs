namespace CustomModbusSlave.Es2gClimatic.Shared.MukCondenser.Request16 {
	public interface IMukCondenserWorkmodeCommandFromKsm {
		bool RegulatorIsWorking { get; }
		bool RegulatorIsWorkingParking { get; }
		bool Washing { get; }
		bool FanSpeedIsMax { get; }
		bool FanIsOff { get; }
		bool ManualMode { get; }
		bool ForceTurnOnSecondStageOnly { get; }
	}
}