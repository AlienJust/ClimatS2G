using System.Collections.Generic;
using AlienJust.Support.Conversion;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	class MukWarmFloorReply03DataBuilder : IBuilder<IMukWarmFloorReply03Data> {
		private readonly IList<byte> _data;

		public MukWarmFloorReply03DataBuilder(IList<byte> reply) {
			_data = reply;
		}

		public IMukWarmFloorReply03Data Build() {
			return new MukWarmFloorReply03Data {
				PwmHeat = _data[3] * 256 + _data[4],
				AnalogueInput = _data[5] * 256 + _data[6],
				TemperatureRegulatorWorkMode = (_data[7] * 256 + _data[8]) * 0.01,
				ByteOfIncomingSignals = _data[10],
				ByteOfOutgoingSignals = _data[12],
				AutomaticModeStage = new RawAndConvertedValues<int, MukWarmFloorAutomaticModeStage>(_data[13] * 256 + _data[14], new MukWarmFloorAutomaticModeStageFromIntBuilder()),
				CalculatedTemperatureSetting = (_data[15] * 256 + _data[14]) * 0.01,
				MukWarmFloorDiagnostic1 = new RawAndConvertedValues<int, IMukWarmFloorDiagnostic1>(_data[17] * 256 + _data[18], new MukWarmFloorDiagnostic1Builder()),
				MukWarmFloorDiagnostic2 = new RawAndConvertedValues<int, IMukWarmFloorDiagnostic2>(_data[19] * 256 + _data[20], new MukWarmFloorDiagnostic2Builder()),
				FirmwareVersion = _data[21] * 256 + _data[22]
			};


		}
	}
}
