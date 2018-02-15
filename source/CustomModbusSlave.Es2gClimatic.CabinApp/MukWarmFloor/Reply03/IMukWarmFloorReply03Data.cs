using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	interface IMukWarmFloorReply03Data
	{
		int PwmHeat { get; }
		int AnalogueInput { get; }
		int TemperatureRegulatorWorkMode { get; }
		int ByteOfIncomingSignals { get; }
		int ByteOfOutgoingSignals { get; }
		MukWarmFloorAutomaticModeStage AutomaticModeStage { get; }
		int CalculatedTemperatureSetting { get; }
		IMukWarmFloorDiagnostic1 MukWarmFloorDiagnostic1 { get; }
		IMukWarmFloorDiagnostic2 MukWarmFloorDiagnostic2 { get; }
		int FirmwareVersion { get; }
	}

	interface IMukWarmFloorDiagnostic1
	{
		bool ThermoResistorIsLost { get; }
	}

	/// <summary>
	/// Future in near
	/// </summary>
	interface IMukWarmFloorDiagnostic2 {
		bool ThermoResistorIsLost { get; }
	}


	enum MukWarmFloorAutomaticModeStage
	{
		ControllerInitialization,
		HeatModeIsOff,
		HeatModeIsOn
	}
}
