using System;
using System.Linq;
using System.Text;
using AlienJust.Support.Conversion;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	interface IMukWarmFloorReply03Data {
		int PwmHeat { get; }
		int AnalogueInput { get; }
		double TemperatureRegulatorWorkMode { get; }
		int ByteOfIncomingSignals { get; }
		int ByteOfOutgoingSignals { get; }
		RawAndConvertedValues<int, MukWarmFloorAutomaticModeStage> AutomaticModeStage { get; }
		int CalculatedTemperatureSetting { get; }
		RawAndConvertedValues<int, IMukWarmFloorDiagnostic1> MukWarmFloorDiagnostic1 { get; }
		RawAndConvertedValues<int, IMukWarmFloorDiagnostic2> MukWarmFloorDiagnostic2 { get; }
		int FirmwareVersion { get; }
	}

	class MukWarmFloorReply03Data : IMukWarmFloorReply03Data
	{
		public int PwmHeat { get; set; }
		public int AnalogueInput { get; set; }
		public double TemperatureRegulatorWorkMode { get; set; }
		public int ByteOfIncomingSignals { get; set; }
		public int ByteOfOutgoingSignals { get; set; }
		public RawAndConvertedValues<int, MukWarmFloorAutomaticModeStage> AutomaticModeStage { get; set; }
		public int CalculatedTemperatureSetting { get; set; }
		public RawAndConvertedValues<int, IMukWarmFloorDiagnostic1> MukWarmFloorDiagnostic1 { get; set; }
		public RawAndConvertedValues<int, IMukWarmFloorDiagnostic2> MukWarmFloorDiagnostic2 { get; set; }
		public int FirmwareVersion { get; set; }
	}

	interface IMukWarmFloorDiagnostic1 {
		bool ThermoResistorIsLost { get; }
	}

	/// <summary>
	/// Future in near
	/// </summary>
	interface IMukWarmFloorDiagnostic2 {
		bool ThermoResistorIsLost { get; }
	}


	enum MukWarmFloorAutomaticModeStage {
		ControllerInitialization,
		HeatModeIsOff,
		HeatModeIsOn
	}


}
