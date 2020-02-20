using AlienJust.Support.Conversion.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03
{
	internal sealed class MukWarmFloorReply03Data : IMukWarmFloorReply03Data {
		public int PwmHeat { get; set; }
		public int AnalogueInput { get; set; }
		public double TemperatureRegulatorWorkMode { get; set; }
		public int ByteOfIncomingSignals { get; set; }
		public int ByteOfOutgoingSignals { get; set; }
		public IRawAndConvertedValues<int, MukWarmFloorAutomaticModeStage> AutomaticModeStage { get; set; }
		public double CalculatedTemperatureSetting { get; set; }
		public IRawAndConvertedValues<int, IMukWarmFloorDiagnostic1> MukWarmFloorDiagnostic1 { get; set; }
		public IRawAndConvertedValues<int, IMukWarmFloorDiagnostic2> MukWarmFloorDiagnostic2 { get; set; }
		public int FirmwareVersion { get; set; }
	}
}
