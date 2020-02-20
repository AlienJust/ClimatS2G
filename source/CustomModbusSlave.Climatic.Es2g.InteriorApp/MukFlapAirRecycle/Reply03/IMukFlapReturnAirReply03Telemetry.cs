using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.Diagnostic2;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire.Diagnostic;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	internal interface IMukFlapReturnAirReply03Telemetry {
		int FlapPwmSetting { get; }
		ISensorIndication<double> TemperatureAddress1 { get; }
		ISensorIndication<double> TemperatureAddress2 { get; }
		IMukFlapReturnAirIncomingSignals IncomingSignals { get; }
		byte OutgoingSignals { get; }
		IMukFlapReturnAirOutgoingSignals OutgoingSignalsDescription { get; }
		double AnalogInput { get; }

		IMukFlapWorkmodeStage AutomaticModeStage { get; }
		IMukFlapAirRecycleDiagnostic1 Diagnostic1 { get; }
		IMukFlapDiagnostic2 Diagnostic2 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic3OneWire1 { get; }
		IMukFlapDiagnosticOneWireSensor Diagnostic4OneWire2 { get; }

		int ConcentratorStatus { get; }
		IMukFlapReturnAirConcentratorStatus ConcentratorStatusParsed { get; }
		int ConcentratorDrivers { get; }
		int ConcentratorVoltage { get; }
		int Reserve14 { get; }
		int Reserve15 { get; }
		int FirmwareBuildNumber { get; }
		int Reserve17 { get; }
		int Reserve18 { get; }
	}
}
