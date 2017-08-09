using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts {
	internal interface IMukFlapReply03Telemetry {
		int FlapPosition { get; }
		ISensorIndication<double> TemperatureAddress1 { get; }
		ISensorIndication<double> TemperatureAddress2 { get; }
		IIncomingSignals IncomingSignals { get; }
		byte OutgoingSignals { get; }
		double AnalogInput { get; }
		IMukFlapWorkmodeStage AutomaticModeStage { get; }
		IMukFlapDiagnostic1 Diagnostic1 { get; }
		IMukFlapOuterAirDiagnostic2 Diagnostic2 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic3OneWire1 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire2 { get; }

		IEmersonDiagnosticCircuit1 EmersonDiagnosticCircuit1 { get; }
		ISensorIndication<double> EmersonTemperatureCircuit1 { get; }
		ISensorIndication<double> EmersonPressureCircuit1 { get; }
		int EmersonValveSettingCircuit1 { get; }

		IEmersonDiagnosticCircuit2 EmersonDiagnosticCircuit2 { get; }
		ISensorIndication<double> EmersonTemperatureCircuit2 { get; }
		ISensorIndication<double> EmersonPressureCircuit2 { get; }
		int EmersonValveSettingCircuit2 { get; }

		int FirmwareBuildNumber { get; }
	}
}