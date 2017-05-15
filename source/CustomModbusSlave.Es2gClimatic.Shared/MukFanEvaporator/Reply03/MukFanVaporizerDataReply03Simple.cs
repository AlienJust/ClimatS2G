using CustomModbusSlave.Es2gClimatic.Shared.EmersionDiagnostic;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.TemperatureRegulatorWorkMode;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03 {
	internal class MukFanVaporizerDataReply03Simple : IMukFanVaporizerDataReply03 {
		public ushort FanPwm { get; set; }
		public ISensorIndication<double> TemperatureAddress1 { get; set; }
		public ISensorIndication<double> TemperatureAddress2 { get; set; }
		public byte IncomingSignals { get; set; }
		public byte OutgoingSignals { get; set; }
		public ushort AnalogInput { get; set; }
		public ushort HeatingPwm { get; set; }

		public ushort AutomaticModeStage { get; set; }
		public MukFanEvaporatorWorkstage AutomaticModeStageParsed { get; set; }

		public ISensorIndication<double> TemperatureAddress3 { get; set; }

		public ITemperatureRegulatorWorkMode TemperatureRegulatorWorkMode { get; set; }
		public double CalculatedTemperatureSetting { get; set; }
		public ushort FanSpeed { get; set; }

		public ushort Diagnostic1 { get; set; }
		public IMukFanVaporizerDataReply03Diagnostic1 Diagnostic1Parsed { get; set; }

		public ushort Diagnostic2 { get; set; }
		public IMukFanVaporizerDataReply03Diagnostic2 Diagnostic2Parsed { get; set; }

		public ushort Diagnostic3 { get; set; }
		public IMukEmersionSwitchOnDiagnostic Diagnostic3Parsed { get; set; }


		public ushort Diagnostic4 { get; set; }
		public IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire1 { get; set; }

		public ushort Diagnostic5 { get; set; }
		public IMukFlapDiagnosticOneWireSensor Diagnostic5OneWire2 { get; set; }

		public ushort FirmwareBuildNumber { get; set; }
	}
}