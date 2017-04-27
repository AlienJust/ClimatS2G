namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03 {
	internal class MukFanVaporizerDataReply03Diagnostic2Simple : IMukFanVaporizerDataReply03Diagnostic2 {
		public bool PhaseErrorOrPowerLost { get; set; }
		public bool ReserveBit1 { get; set; }
		public bool PowerSuplyOverheat { get; set; }
		public bool ReserveBit3 { get; set; }
		public bool FaultAttribute { get; set; }
		public bool MotorOverheat { get; set; }
		public bool HallSensorError { get; set; }
		public bool MotorLock { get; set; }

		public bool DcCurrentOverload { get; set; }
		public bool ControllerOverheat { get; set; }
		public bool DriverError { get; set; }
		public bool DcOvervoltage { get; set; }
		public bool DcLowVoltage { get; set; }
		public bool LowVoltage { get; set; }
		public bool ReserveBit14 { get; set; }
		public bool Lock { get; set; }
	}
}