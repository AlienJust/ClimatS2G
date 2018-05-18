namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16 {
	internal interface IControlOfHiVoltageContactor {
		bool ModeHeatOrCool { get; }
		bool SegmentTypeMasterOrSlave { get; }
		bool Step1HeatTurnOn { get; }
		bool Step2HeatTurnOn { get; }
	}
}