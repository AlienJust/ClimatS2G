using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts {
	interface IMukAirExhausterReply03Data {
		int HeatPwm { get; } // TODO: rename to FanPwm
		ISensorIndication<double> TemperatureOneWire { get; }
		int InputSignals { get; }
		int OutputSignals { get; }
		double AnalogInputCo2 { get; }
		IAutomaticWorkmodeStage WorkmodeStage { get; }
		int FanSpeed { get; }
		int Diagnostic1 { get; }
		int Diagnostic2Fan { get; }
		int Diagnostic3OneWire { get; }
		int FirmwareBuildNumber { get; }
		int Reserve11 { get; }
		int Reserve12 { get; }
	}
}
