using AlienJust.Support.Conversion;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	interface IMukWarmFloorReply03Data {
		int PwmHeat { get; }
		int AnalogueInput { get; }
		double TemperatureRegulatorWorkMode { get; }
		int ByteOfIncomingSignals { get; }
		int ByteOfOutgoingSignals { get; }
		RawAndConvertedValues<int, MukWarmFloorAutomaticModeStage> AutomaticModeStage { get; }
		double CalculatedTemperatureSetting { get; }
		RawAndConvertedValues<int, IMukWarmFloorDiagnostic1> MukWarmFloorDiagnostic1 { get; }
		RawAndConvertedValues<int, IMukWarmFloorDiagnostic2> MukWarmFloorDiagnostic2 { get; }
		int FirmwareVersion { get; }
	}
}
