using CustomModbusSlave.MicroclimatEs2gApp.Common.BsSm;

namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class BsSmDataCommand32RequestSimple : IBsSmDataCommand32Request {
		private readonly int _temperatureInsideTheCabin;
		private readonly int _temperatureOutdoor;
		private readonly int _fanSpeed;
		private readonly bool _isTunelModeOn;
		private readonly bool _isWarmFloorOn;
		private readonly ClimaticSystemWorkMode _currentClimaticWorkMode;
		private readonly int _fault1;
		private readonly int _fault2;
		private readonly int _fault3;
		private readonly int _fault4;
		private readonly int _fault5;

		public BsSmDataCommand32RequestSimple(int temperatureInsideTheCabin, int temperatureOutdoor, int fanSpeed, bool isTunelModeOn, bool isWarmFloorOn, ClimaticSystemWorkMode currentClimaticWorkMode, int fault1, int fault2, int fault3, int fault4, int fault5) {
			_temperatureInsideTheCabin = temperatureInsideTheCabin;
			_temperatureOutdoor = temperatureOutdoor;
			_fanSpeed = fanSpeed;
			_isTunelModeOn = isTunelModeOn;
			_isWarmFloorOn = isWarmFloorOn;
			_currentClimaticWorkMode = currentClimaticWorkMode;
			_fault1 = fault1;
			_fault2 = fault2;
			_fault3 = fault3;
			_fault4 = fault4;
			_fault5 = fault5;

			// TODO:  see http://stackoverflow.com/questions/1681406/wpf-databinding-watch-for-thrown-exceptions
		}

		public int TemperatureInsideTheCabin {
			get { return _temperatureInsideTheCabin; }
		}

		public int TemperatureOutdoor {
			get { return _temperatureOutdoor; }
		}

		public int FanSpeed {
			get { return _fanSpeed; }
		}

		public bool IsTunelModeOn {
			get { return _isTunelModeOn; }
		}

		public bool IsWarmFloorOn {
			get { return _isWarmFloorOn; }
		}

		public ClimaticSystemWorkMode CurrentClimaticWorkMode {
			get { return _currentClimaticWorkMode; }
		}

		public int Fault1 {
			get { return _fault1; }
		}

		public int Fault2 {
			get { return _fault2; }
		}

		public int Fault3 {
			get { return _fault3; }
		}

		public int Fault4 {
			get { return _fault4; }
		}

		public int Fault5 {
			get { return _fault5; }
		}
	}
}