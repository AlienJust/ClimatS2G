using AlienJust.Support.Conversion.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	interface IMukWarmFloorReply03Data {
		int PwmHeat { get; }
		int AnalogueInput { get; }
		double TemperatureRegulatorWorkMode { get; }
		int ByteOfIncomingSignals { get; }
		int ByteOfOutgoingSignals { get; }
		IRawAndConvertedValues<int, MukWarmFloorAutomaticModeStage> AutomaticModeStage { get; }
		double CalculatedTemperatureSetting { get; }
		IRawAndConvertedValues<int, IMukWarmFloorDiagnostic1> MukWarmFloorDiagnostic1 { get; }
		IRawAndConvertedValues<int, IMukWarmFloorDiagnostic2> MukWarmFloorDiagnostic2 { get; }
		int FirmwareVersion { get; }
	}
}
