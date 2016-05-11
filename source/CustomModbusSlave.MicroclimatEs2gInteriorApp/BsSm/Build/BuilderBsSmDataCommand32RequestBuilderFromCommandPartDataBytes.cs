using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class BsSmDataCommand32RequestBuilderFromCommandPartDataBytes : IBuilder<IBsSmDataCommand32Request> {
		private readonly IList<byte> _commandPartDataBytes;
		public BsSmDataCommand32RequestBuilderFromCommandPartDataBytes(IList<byte> commandPartDataBytes) {
			_commandPartDataBytes = commandPartDataBytes;
		}

		public IBsSmDataCommand32Request Build() {
			// int TemperatureInsideTheCabin
			// int TemperatureOutdoor
			// int FanSpeed
			// bool IsTunelModeOn
			// bool IsWarmFloorOn
			// ClimaticSystemWorkMode CurrentClimaticWorkMode
			// uint Fault1 { get; }
			// uint Fault2 { get; }
			// uint Fault3 { get; }
			// uint Fault4 { get; }
			// uint Fault5 { get; }

			// first byte is bytes count

			var maximumPower = _commandPartDataBytes[1];
			
			var temperatureInterior = _commandPartDataBytes[2] - 60;
			var temperatureOutdoor = _commandPartDataBytes[3] - 60;
			var temperatureDelta = _commandPartDataBytes[4];
			var workMode = new WorkModeBuilderFromByte(_commandPartDataBytes[5]).Build();
			var workMode2= new WorkMode2BuilderFromByte(_commandPartDataBytes[6]).Build();

			var powerStage1 = _commandPartDataBytes[7];
			var powerStage2 = _commandPartDataBytes[8];
			var powerStage3 = _commandPartDataBytes[9];
			var fault1 = _commandPartDataBytes[10] + _commandPartDataBytes[11] * 256;
			var fault2 = _commandPartDataBytes[12] + _commandPartDataBytes[13] * 256;
			var fault3 = _commandPartDataBytes[14] + _commandPartDataBytes[15] * 256;
			var fault4 = _commandPartDataBytes[16] + _commandPartDataBytes[17] * 256;
			var fault5 = _commandPartDataBytes[18] + _commandPartDataBytes[19] * 256;

			var reserve23OrAvgTemperature = _commandPartDataBytes[20];
			var reserve24 = _commandPartDataBytes[21];
			var reserve25 = _commandPartDataBytes[22];

			return new BsSmDataCommand32RequestSimple(
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
				reserve24,
				reserve25);
		}
	}
}