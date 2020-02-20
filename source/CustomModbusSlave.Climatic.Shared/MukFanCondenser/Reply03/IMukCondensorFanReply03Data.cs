using CustomModbusSlave.Es2gClimatic.Shared.OneWire;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanCondenser.Reply03 {
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
		ISensorIndication<double> CondensingPressure2 { get; }

		bool Stage1IsOn { get; }
		bool Stage2IsOn { get; }
	}
}
