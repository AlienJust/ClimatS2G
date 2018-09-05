using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.Diagnostic2;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

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

	internal interface IMukFlapReturnAirOutgoingSignals {
		bool TurnContactor1On { get; }
		bool TurnContactor2On { get; }
		//РІРєР»СЋС‡РµРЅРёРµ РєРѕРЅС‚Р°РєС‚РѕСЂР° 1		
		//РІРєР»СЋС‡РµРЅРёРµ РєРѕРЅС‚Р°РєС‚РѕСЂР° 2
	}

	internal sealed class MukFlapReturnAirOutgoingSignals : IMukFlapReturnAirOutgoingSignals {
		public bool TurnContactor1On { get; }
		public bool TurnContactor2On { get; }
		
		public MukFlapReturnAirOutgoingSignals(bool turnContactor1On, bool turnContactor2On) {
			TurnContactor1On = turnContactor1On;
			TurnContactor2On = turnContactor2On;
		}
	}
}
