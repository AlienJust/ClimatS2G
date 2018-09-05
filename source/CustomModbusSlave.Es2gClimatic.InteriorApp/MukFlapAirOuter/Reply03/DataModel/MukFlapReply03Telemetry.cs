using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel {
	class MukFlapReply03Telemetry : IMukFlapOuterAirReply03Telemetry {
		public MukFlapReply03Telemetry(int flapPosition, ISensorIndication<double> temperatureAddress1
			, ISensorIndication<double> temperatureAddress2, IIncomingSignals incomingSignals, byte outgoingSignals
			, double analogInput, IMukFlapWorkmodeStage automaticModeStage
			, IMukFlapDiagnostic1 diagnostic1, IMukFlapOuterAirDiagnostic2 diagnostic2, IMukFlapDiagnosticOneWireSensor diagnostic3OneWire1
			, IMukFlapDiagnosticOneWireSensor diagnostic4OneWire2
			, IEmersonDiagnosticCircuit1 emersonDiagnosticCircuit1, ISensorIndication<double> emersonTemperatureCircuit1, ISensorIndication<double> emersonPressureCircuit1
			, int emersonValveSettingCircuit1
			, IEmersonDiagnosticCircuit2 emersonDiagnosticCircuit2, ISensorIndication<double> emersonTemperatureCircuit2, ISensorIndication<double> emersonPressureCircuit2
			, int emersonValveSettingCircuit2
			, int firmwareBuildNumber) {
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
			EmersonDiagnosticCircuit1 = emersonDiagnosticCircuit1;
			EmersonTemperatureCircuit1 = emersonTemperatureCircuit1;
			EmersonPressureCircuit1 = emersonPressureCircuit1;
			EmersonValveSettingCircuit1 = emersonValveSettingCircuit1;

			EmersonDiagnosticCircuit2 = emersonDiagnosticCircuit2;
			EmersonTemperatureCircuit2 = emersonTemperatureCircuit2;
			EmersonPressureCircuit2 = emersonPressureCircuit2;
			EmersonValveSettingCircuit2 = emersonValveSettingCircuit2;

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
		public IMukFlapOuterAirDiagnostic2 Diagnostic2 { get; }
		public IMukFlapDiagnosticOneWireSensor Diagnostic3OneWire1 { get; }
		public IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire2 { get; }

		public IEmersonDiagnosticCircuit1 EmersonDiagnosticCircuit1 { get; }
		public ISensorIndication<double> EmersonTemperatureCircuit1 { get; }
		public ISensorIndication<double> EmersonPressureCircuit1 { get; }
		public int EmersonValveSettingCircuit1 { get; }

		public IEmersonDiagnosticCircuit2 EmersonDiagnosticCircuit2 { get; }
		public ISensorIndication<double> EmersonTemperatureCircuit2 { get; }
		public ISensorIndication<double> EmersonPressureCircuit2 { get; }
		public int EmersonValveSettingCircuit2 { get; }

		public int FirmwareBuildNumber { get; }
	}
}
