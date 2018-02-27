using System;
using System.Collections.Generic;
using AlienJust.Support.Conversion;
using AlienJust.Support.Conversion.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	class MukWarmFloorReply03DataBuilder : IBuilder<IMukWarmFloorReply03Data> {
		private readonly IList<byte> data;

		public MukWarmFloorReply03DataBuilder(IList<byte> reply) {
			data = reply;
		}

		public IMukWarmFloorReply03Data Build() {
			return new MukWarmFloorReply03Data {
				PwmHeat = data[3] * 256 + data[4],
				AnalogueInput = data[5] * 256 + data[6],
				TemperatureRegulatorWorkMode = (data[7] * 256 + data[8]) * 0.01,
				ByteOfIncomingSignals = data[10],
				ByteOfOutgoingSignals = data[12],
				AutomaticModeStage = new RawAndConvertedValues<int, MukWarmFloorAutomaticModeStage>(data[13] * 256 + data[14], new MukWarmFloorAutomaticModeStageFromIntBuilder()),
				CalculatedTemperatureSetting = (data[15] * 256 + data[14]) * 0.01,
				MukWarmFloorDiagnostic1 = new RawAndConvertedValues<int, IMukWarmFloorDiagnostic1>(data[17] * 256 + data[18], new MukWarmFloorDiagnostic1Builder()),
				MukWarmFloorDiagnostic2 = new RawAndConvertedValues<int, IMukWarmFloorDiagnostic2>(data[19] * 256 + data[20], new MukWarmFloorDiagnostic2Builder()),
				FirmwareVersion = data[21] * 256 + data[22]
			};


		}
	}
}