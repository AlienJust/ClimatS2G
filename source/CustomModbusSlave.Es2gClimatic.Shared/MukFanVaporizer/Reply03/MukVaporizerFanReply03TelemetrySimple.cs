using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.TemperatureRegulatorWorkMode;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Reply03 {
	internal class MukVaporizerFanReply03TelemetrySimple : IMukVaporizerFanReply03Telemetry {
		public ushort FanPwm { get; set; }
		public ISensorIndication<double> TemperatureAddress1 { get; set; }
		public ISensorIndication<double> TemperatureAddress2 { get; set; }
		public byte IncomingSignals { get; set; }
		public byte OutgoingSignals { get; set; }
		public ushort AnalogInput { get; set; }
		public ushort HeatingPwm { get; set; }
		public ushort AutomaticModeStage { get; set; }
		public ITemperatureRegulatorWorkMode TemperatureRegulatorWorkMode { get; set; }
		public double CalculatedTemperatureSetting { get; set; }
		public ushort FanSpeed { get; set; }
		public ushort Diagnostic1 { get; set; }
		public ushort Diagnostic2 { get; set; }
		public ushort Diagnostic3 { get; set; }
		public ushort Diagnostic4 { get; set; }
		public ushort Diagnostic5 { get; set; }
		public ushort FirmwareBuildNumber { get; set; }
	}
}