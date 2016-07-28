using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapReturnAir.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.SensorIndications;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapReturnAir.DataModel.SimpleReleases {
	class MukFlapReturnAirReply03Telemetry : IMukFlapReturnAirReply03Telemetry {
		public MukFlapReturnAirReply03Telemetry(int flapPwmSetting, ISensorIndication<double> temperatureAddress1, ISensorIndication<double> temperatureAddress2, IIncomingSignals incomingSignals, byte outgoingSignals, double analogInput, IMukFlapWorkmodeStage automaticModeStage, IMukFlapDiagnostic1 diagnostic1, IMukFlapDiagnostic2 diagnostic2, IMukFlapDiagnosticOneWireSensor diagnostic3OneWire1, IMukFlapDiagnosticOneWireSensor diagnostic4OneWire2, int concentratorStatus, int concentratorDrivers, int concentratorVoltage, int reserve14, int reserve15, int firmwareBuildNumber, int reserve17, int reserve18) {
			FlapPwmSetting = flapPwmSetting;
			TemperatureAddress1 = temperatureAddress1;
			TemperatureAddress2 = temperatureAddress2;
			IncomingSignals = incomingSignals;
			OutgoingSignals = outgoingSignals;
			AnalogInput = analogInput;
			AutomaticModeStage = automaticModeStage;
			Diagnostic1 = diagnostic1;
			Diagnostic2 = diagnostic2;
			Diagnostic3OneWire1 = diagnostic3OneWire1;
			Diagnostic4OneWire2 = diagnostic4OneWire2;
			ConcentratorStatus = concentratorStatus;
			ConcentratorDrivers = concentratorDrivers;
			ConcentratorVoltage = concentratorVoltage;
			Reserve14 = reserve14;
			Reserve15 = reserve15;
			FirmwareBuildNumber = firmwareBuildNumber;
			Reserve17 = reserve17;
			Reserve18 = reserve18;
		}

		public int FlapPwmSetting { get; }
		public ISensorIndication<double> TemperatureAddress1 { get; }
		public ISensorIndication<double> TemperatureAddress2 { get; }
		public IIncomingSignals IncomingSignals { get; }
		public byte OutgoingSignals { get; }
		public double AnalogInput { get; }
		public IMukFlapWorkmodeStage AutomaticModeStage { get; }
		public IMukFlapDiagnostic1 Diagnostic1 { get; }
		public IMukFlapDiagnostic2 Diagnostic2 { get; }
		public IMukFlapDiagnosticOneWireSensor Diagnostic3OneWire1 { get; }
		public IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire2 { get; }
		public int ConcentratorStatus { get; }
		public int ConcentratorDrivers { get; }
		public int ConcentratorVoltage { get; }
		public int Reserve14 { get; }
		public int Reserve15 { get; }
		public int FirmwareBuildNumber { get; }
		public int Reserve17 { get; }
		public int Reserve18 { get; }
	}
}