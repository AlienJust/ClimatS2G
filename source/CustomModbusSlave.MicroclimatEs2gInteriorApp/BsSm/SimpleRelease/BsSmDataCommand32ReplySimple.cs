namespace CustomModbusSlave.MicroclimatEs2gApp.BsSm {
	class BsSmDataCommand32ReplySimple : IBsSmDataCommand32Reply {
		private readonly int _targetTemperatureInsideTheCabin;
		private readonly int _fanSpeedLevel;
		private readonly bool _isWarmFloorOn;
		private readonly uint _astronomicTime;
		private readonly uint _delayedStartTime;
		private readonly int _temperatureOutdoor;
		private readonly IBsSmState _bsSmState;
		private readonly int _bsSmVersionNumber;

		public BsSmDataCommand32ReplySimple(int targetTemperatureInsideTheCabin, int fanSpeedLevel, bool isWarmFloorOn, uint astronomicTime, uint delayedStartTime, int temperatureOutdoor, IBsSmState bsSmState, int bsSmVersionNumber) {
			_targetTemperatureInsideTheCabin = targetTemperatureInsideTheCabin;
			_fanSpeedLevel = fanSpeedLevel;
			_isWarmFloorOn = isWarmFloorOn;
			_astronomicTime = astronomicTime;
			_delayedStartTime = delayedStartTime;
			_temperatureOutdoor = temperatureOutdoor;
			_bsSmState = bsSmState;
			_bsSmVersionNumber = bsSmVersionNumber;
		}

		public int TargetTemperatureInsideTheCabin {
			get { return _targetTemperatureInsideTheCabin; }
		}

		public int FanSpeedLevel {
			get { return _fanSpeedLevel; }
		}

		public bool IsWarmFloorOn {
			get { return _isWarmFloorOn; }
		}

		public uint AstronomicTime {
			get { return _astronomicTime; }
		}

		public uint DelayedStartTime {
			get { return _delayedStartTime; }
		}

		public int TemperatureOutdoor {
			get { return _temperatureOutdoor; }
		}

		public IBsSmState BsSmState {
			get { return _bsSmState; }
		}

		public int BsSmVersionNumber {
			get { return _bsSmVersionNumber; }
		}
	}
}