using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.MicroclimatEs2gApp.SensorIndications;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Contracts {
	internal interface IMukFlapReply03Telemetry {
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