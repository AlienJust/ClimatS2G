using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.Diagnostic2;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts {
	internal interface IMukFlapAirReply03Telemetry {
		int FlapPosition { get; }
		ISensorIndication<double> TemperatureAddress1 { get; }
		ISensorIndication<double> TemperatureAddress2 { get; }
		IIncomingSignals IncomingSignals { get; }
		byte OutgoingSignals { get; }
		double AnalogInput { get; }
		IMukFlapWorkmodeStage AutomaticModeStage { get; }
		IMukFlapDiagnostic1 Diagnostic1 { get; }
		IMukFlapDiagnostic2 Diagnostic2 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic3OneWire1 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire2 { get; }
		IEmersonDiagnostic EmersonDiagnostic { get; }
		double EmersonTemperature { get; }
		double EmersonPressure { get; }
		int EmersonValveSetting { get; }
		int FirmwareBuildNumber { get; }
	}
}