using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.Diagnostic2;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.SimpleRelease {
	class MukFlapReply03Telemetry : IMukFlapReply03Telemetry {
		public MukFlapReply03Telemetry(int flapPosition, ISensorIndication<double> temperatureAddress1, ISensorIndication<double> temperatureAddress2, IIncomingSignals incomingSignals, byte outgoingSignals, double analogInput, IMukFlapWorkmodeStage automaticModeStage, IMukFlapDiagnostic1 diagnostic1, IMukFlapDiagnostic2 diagnostic2, IMukFlapDiagnosticOneWireSensor diagnostic3OneWire1, IMukFlapDiagnosticOneWireSensor diagnostic4OneWire2, IEmersonDiagnostic emersonDiagnostic, double emersonTemperature, double emersonPressure, int emersonValveSetting, int firmwareBuildNumber) {
			FlapPosition = flapPosition;
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
			EmersonDiagnostic = emersonDiagnostic;
			EmersonTemperature = emersonTemperature;
			EmersonPressure = emersonPressure;
			EmersonValveSetting = emersonValveSetting;
			FirmwareBuildNumber = firmwareBuildNumber;
		}

		public int FlapPosition { get; }
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
		public IEmersonDiagnostic EmersonDiagnostic { get; }
		public double EmersonTemperature { get; }
		public double EmersonPressure { get; }
		public int EmersonValveSetting { get; }
		public int FirmwareBuildNumber { get; }
	}
}