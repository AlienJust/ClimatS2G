using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.SensorIndications;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer.DataModel.Contracts {
	internal interface IMukFlapWinterSummerReply03Telemetry {
		int FlapPwmSetting { get; }
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

		int Reserve11 { get; }
		int Reserve12 { get; }
		int Reserve13 { get; }
		int Reserve14 { get; }
		int Reserve15 { get; }
		int Reserve16 { get; }
		int Reserve17 { get; }
		int Reserve18 { get; }
		int FirmwareBuildNumber { get; }
		int Reserve20 { get; }
	}
}