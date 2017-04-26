using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.TemperatureRegulatorWorkMode;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Reply03 {
	public interface IMukVaporizerFanReply03Telemetry {
		ushort FanPwm { get; }
		ISensorIndication<double> TemperatureAddress1 { get; }
		ISensorIndication<double> TemperatureAddress2 { get; }
		byte IncomingSignals { get; }
		byte OutgoingSignals { get; }

		ushort AnalogInput { get; }
		ushort HeatingPwm { get; }
		ushort AutomaticModeStage { get; }
		ITemperatureRegulatorWorkMode TemperatureRegulatorWorkMode { get; }
		double CalculatedTemperatureSetting { get; }
		ushort FanSpeed { get; }
		ushort Diagnostic1 { get; }
		ushort Diagnostic2 { get; }
		ushort Diagnostic3 { get; }
		ushort Diagnostic4 { get; }
		ushort Diagnostic5 { get; }

		ushort FirmwareBuildNumber { get; }
	}
}
