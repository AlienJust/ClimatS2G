using CustomModbusSlave.MicroclimatEs2gApp.SensorIndications;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts {
	interface IReply03Data {
		int HeatPwm { get; }
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
