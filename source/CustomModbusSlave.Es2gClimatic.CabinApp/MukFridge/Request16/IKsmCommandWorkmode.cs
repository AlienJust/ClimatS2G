namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFridge.Request16 {
	internal interface IKsmCommandWorkmode {
		bool RegulatorIsWorking { get; }
		// zb bit #2 is skipped
		bool RegulatorIsWorkingParking { get; }
		bool Washing { get; }
		bool FanSpeedIsMax { get; }
		bool FanIsOff { get; }
		bool ManualMode { get; }
	}
}