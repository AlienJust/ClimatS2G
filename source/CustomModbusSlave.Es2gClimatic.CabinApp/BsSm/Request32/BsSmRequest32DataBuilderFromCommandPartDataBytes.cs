using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.BsSm.Request32 {
	class BsSmRequest32DataBuilderFromCommandPartDataBytes : Shared.IBuilder<IBsSmRequest32Data> {
		private readonly IList<byte> _commandPartDataBytes;
		public BsSmRequest32DataBuilderFromCommandPartDataBytes(IList<byte> commandPartDataBytes) {
			_commandPartDataBytes = commandPartDataBytes;
		}

		public IBsSmRequest32Data Build() {
			var temperatureInsideTheCabin = _commandPartDataBytes[1] - 60;
			var temperatureOutdoor = _commandPartDataBytes[2] - 60;
			var fanSpeed = (_commandPartDataBytes[3] & 0x03);
			var isTunelModeOn = (_commandPartDataBytes[3] & 0x04) == 0x04;
			var isWarmfloorOn = (_commandPartDataBytes[3] & 0x08) == 0x08;
			var currentClimaticWorkMode = new Shared.BsSm.ClimaticSystemWorkModeBuilderFromInt((_commandPartDataBytes[3] & 0xF0) >> 4).Build();
			var fault1 = _commandPartDataBytes[4] + _commandPartDataBytes[5] * 256;
			var fault2 = _commandPartDataBytes[6] + _commandPartDataBytes[7] * 256;
			var fault3 = _commandPartDataBytes[8] + _commandPartDataBytes[9] * 256;
			var fault4 = _commandPartDataBytes[10] + _commandPartDataBytes[11] * 256;
			var fault5 = _commandPartDataBytes[12] + _commandPartDataBytes[13] * 256;
			return  new BsSmRequest32DataSimple(
				temperatureInsideTheCabin,
				temperatureOutdoor,
				fanSpeed,
				isTunelModeOn,
				isWarmfloorOn,
				currentClimaticWorkMode,
				fault1,
				fault2,
				fault3,
				fault4,
				fault5);
		}
	}
}