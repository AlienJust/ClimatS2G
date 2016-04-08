using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapReply03Telemetry : IMukFlapReply03Telemetry {
		public MukFlapReply03Telemetry(int flapPosition, double temperatureAddress1, double temperatureAddress2, IIncomingSignals incomingSignals, byte outgoingSignals, double analogInput, IMukFlapWorkmodeStage automaticModeStage, IMukFlapDiagnostic1 diagnostic1, IMukFlapDiagnostic2 diagnostic2, IMukFlapDiagnosticOneWireSensor diagnostic3OneWire1, IMukFlapDiagnosticOneWireSensor diagnostic4OneWire2, IEmersonDiagnostic emersonDiagnostic, double emersonTemperature, double emersonPressure, int emersonValveSetting, int firmwareBuildNumber) {
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
		public double TemperatureAddress1 { get; }
		public double TemperatureAddress2 { get; }
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