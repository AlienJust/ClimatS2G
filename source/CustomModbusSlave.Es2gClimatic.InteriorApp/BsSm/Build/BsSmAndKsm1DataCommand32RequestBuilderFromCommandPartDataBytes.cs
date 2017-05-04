using System.Collections.Generic;
using System.Linq;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.SimpleRelease;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.BsSm.Build {
	class BsSmAndKsm1DataCommand32RequestBuilderFromCommandPartDataBytes : IBuilder<IBsSmAndKsm1DataCommand32Request> {
		private readonly IList<byte> _commandPartDataBytes;
		public BsSmAndKsm1DataCommand32RequestBuilderFromCommandPartDataBytes(IList<byte> commandPartDataBytes) {
			_commandPartDataBytes = commandPartDataBytes.Skip(2).Take(commandPartDataBytes.Count - 4).ToList();
		}

		public IBsSmAndKsm1DataCommand32Request Build() {
			var maximumPower = _commandPartDataBytes[1];
			
			var temperatureInterior = _commandPartDataBytes[2] - 60;
			var temperatureOutdoor = _commandPartDataBytes[3] - 60;
			var temperatureDelta = _commandPartDataBytes[4] - 60;
			var workMode = new WorkModeInRequestBuilderFromByte(_commandPartDataBytes[5]).Build();
			var workMode2= new WorkModeInRequest2BuilderFromByte(_commandPartDataBytes[6]).Build();

			var powerStage1 = _commandPartDataBytes[7];
			var powerStage2 = _commandPartDataBytes[8];
			var powerStage3 = _commandPartDataBytes[9];
			var fault1 = _commandPartDataBytes[10] + _commandPartDataBytes[11] * 256;
			var fault2 = _commandPartDataBytes[12] + _commandPartDataBytes[13] * 256;
			var fault3 = _commandPartDataBytes[14] + _commandPartDataBytes[15] * 256;
			var fault4 = _commandPartDataBytes[16] + _commandPartDataBytes[17] * 256;
			var fault5 = _commandPartDataBytes[18] + _commandPartDataBytes[19] * 256;

			var reserve23OrAvgTemperature = _commandPartDataBytes[20];
			var сo2LevelInCurrentSegment = _commandPartDataBytes[21] * 10.0;

			var ch2EmersonCompressorPressureIsLow = _commandPartDataBytes[22].GetBit(3);
			var reserve25 = _commandPartDataBytes[22];

			return new BsSmAndKsm1DataCommand32RequestSimple(
				maximumPower,
				temperatureInterior,
				temperatureOutdoor,
				temperatureDelta,
				workMode,
				workMode2,
				powerStage1,
				powerStage2,
				powerStage3,
				fault1,
				fault2,
				fault3,
				fault4,
				fault5,
				reserve23OrAvgTemperature,
				сo2LevelInCurrentSegment,
				reserve25,
				ch2EmersonCompressorPressureIsLow);
		}
	}
}