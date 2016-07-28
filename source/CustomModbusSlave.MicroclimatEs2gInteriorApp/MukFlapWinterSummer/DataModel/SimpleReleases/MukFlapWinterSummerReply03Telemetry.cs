using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.SensorIndications;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer.DataModel.SimpleReleases {
	class MukFlapWinterSummerReply03Telemetry : IMukFlapWinterSummerReply03Telemetry {
		public MukFlapWinterSummerReply03Telemetry(int flapPwmSetting, ISensorIndication<double> temperatureAddress1, ISensorIndication<double> temperatureAddress2, IIncomingSignals incomingSignals, byte outgoingSignals, double analogInput, IMukFlapWorkmodeStage automaticModeStage, IMukFlapDiagnostic1 diagnostic1, IMukFlapDiagnostic2 diagnostic2, IMukFlapDiagnosticOneWireSensor diagnostic3OneWire1, IMukFlapDiagnosticOneWireSensor diagnostic4OneWire2, int reserve11, int reserve12, int reserve13, int reserve14, int reserve15, int reserve16, int reserve17, int reserve18, int firmwareBuildNumber, int reserve20) {
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
			Reserve11 = reserve11;
			Reserve12 = reserve12;
			Reserve13 = reserve13;
			Reserve14 = reserve14;
			Reserve15 = reserve15;
			Reserve16 = reserve16;
			Reserve17 = reserve17;
			Reserve18 = reserve18;
			FirmwareBuildNumber = firmwareBuildNumber;
			Reserve20 = reserve20;
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
		public int Reserve11 { get; }
		public int Reserve12 { get; }
		public int Reserve13 { get; }
		public int Reserve14 { get; }
		public int Reserve15 { get; }
		public int Reserve16 { get; }
		public int Reserve17 { get; }
		public int Reserve18 { get; }
		public int FirmwareBuildNumber { get; }
		public int Reserve20 { get; }
	}
}