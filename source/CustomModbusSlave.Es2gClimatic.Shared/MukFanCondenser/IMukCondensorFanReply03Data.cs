using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge {
	public interface IMukCondensorFanReply03Data {
		ushort FanPwm { get; }

		ISensorIndication<double> CondensingPressure { get; }

		byte IncomingSignals { get; }
		byte OutgoingSignals { get; }

		ushort AnalogInput { get; }
		ushort AutomaticModeStage { get; }
		ushort WorkMode { get; }
		ushort Diagnostic1 { get; }
		ushort Diagnostic2 { get; }
		ushort FanSpeed { get; }
		ushort FirmwareBuildNumber { get; }

		bool Stage1IsOn { get; }
		bool Stage2IsOn { get; }
	}

	class MukCondensorFanReply03DataSimple : IMukCondensorFanReply03Data {
		public ushort FanPwm { get; set; }
		public ISensorIndication<double> CondensingPressure { get; set; }
		public byte IncomingSignals { get; set; }
		public byte OutgoingSignals { get; set; }
		public ushort AnalogInput { get; set; }
		public ushort AutomaticModeStage { get; set; }
		public ushort WorkMode { get; set; }
		public ushort Diagnostic1 { get; set; }
		public ushort Diagnostic2 { get; set; }
		public ushort FanSpeed { get; set; }
		public ushort FirmwareBuildNumber { get; set; }
		public bool Stage1IsOn { get; set; }
		public bool Stage2IsOn { get; set; }
	}
}
