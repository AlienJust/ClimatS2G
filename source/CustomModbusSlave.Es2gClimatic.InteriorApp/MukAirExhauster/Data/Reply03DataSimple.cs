using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data {
	class Reply03DataSimple : IReply03Data {
		public Reply03DataSimple(int heatPwm, ISensorIndication<double> temperatureOneWire, int inputSignals, int outputSignals, double analogInputCo2, IAutomaticWorkmodeStage workmodeStage, int fanSpeed, int diagnostic1, int diagnostic2Fan, int diagnostic3OneWire, int firmwareBuildNumber, int reserve11, int reserve12) {
			HeatPwm = heatPwm;
			TemperatureOneWire = temperatureOneWire;
			InputSignals = inputSignals;
			OutputSignals = outputSignals;
			AnalogInputCo2 = analogInputCo2;
			WorkmodeStage = workmodeStage;
			FanSpeed = fanSpeed;
			Diagnostic1 = diagnostic1;
			Diagnostic2Fan = diagnostic2Fan;
			Diagnostic3OneWire = diagnostic3OneWire;
			FirmwareBuildNumber = firmwareBuildNumber;
			Reserve11 = reserve11;
			Reserve12 = reserve12;
		}

		public int HeatPwm { get; }
		public ISensorIndication<double> TemperatureOneWire { get; }
		public int InputSignals { get; }
		public int OutputSignals { get; }
		public double AnalogInputCo2 { get; }
		public IAutomaticWorkmodeStage WorkmodeStage { get; }
		public int FanSpeed { get; }
		public int Diagnostic1 { get; }
		public int Diagnostic2Fan { get; }
		public int Diagnostic3OneWire { get; }
		public int FirmwareBuildNumber { get; }
		public int Reserve11 { get; }
		public int Reserve12 { get; }
	}
}