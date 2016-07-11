using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.SensorIndications;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapReturnAir.DataModel.Contracts {
	internal interface IMukFlapReturnAirReply03Telemetry {
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

		int ConcentratorStatus { get; }
		int ConcentratorDrivers { get; }
		int ConcentratorVoltage { get; }
		int Reserve14 { get; }
		int Reserve15 { get; }
		int FirmwareBuildNumber { get; }
		int Reserve17 { get; }
		int Reserve18 { get; }
	}
}