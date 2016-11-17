namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFridge.Request16 {
	internal class KsmCommandWorkmodeSimple : IKsmCommandWorkmode {
		public bool RegulatorIsWorking { get; set; }
		public bool RegulatorIsWorkingParking { get; set; }
		public bool Washing { get; set; }
		public bool FanSpeedIsMax { get; set; }
		public bool FanIsOff { get; set; }
		public bool ManualMode { get; set; }
	}
}