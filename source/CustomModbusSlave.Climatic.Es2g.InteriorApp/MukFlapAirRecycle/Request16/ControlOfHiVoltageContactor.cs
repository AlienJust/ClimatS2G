namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16 {
	internal sealed class ControlOfHiVoltageContactor : IControlOfHiVoltageContactor {
		public ControlOfHiVoltageContactor(bool modeHeatOrCool, bool segmentTypeMasterOrSlave, bool step1HeatTurnOn, bool step2HeatTurnOn) {
			ModeHeatOrCool = modeHeatOrCool;
			SegmentTypeMasterOrSlave = segmentTypeMasterOrSlave;
			Step1HeatTurnOn = step1HeatTurnOn;
			Step2HeatTurnOn = step2HeatTurnOn;
		}

		public bool ModeHeatOrCool { get; }
		public bool SegmentTypeMasterOrSlave { get; }
		public bool Step1HeatTurnOn { get; }
		public bool Step2HeatTurnOn { get; }
	}
}
