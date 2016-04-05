using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapDiagnostic1 : IMukFlapDiagnostic1 {
		private readonly bool _noEmersionControllerAnswer;
		private readonly bool _sensorOneWire1Error;
		private readonly bool _sensorOneWire2Error;

		public MukFlapDiagnostic1(bool noEmersionControllerAnswer, bool sensorOneWire1Error, bool sensorOneWire2Error) {
			_noEmersionControllerAnswer = noEmersionControllerAnswer;
			_sensorOneWire1Error = sensorOneWire1Error;
			_sensorOneWire2Error = sensorOneWire2Error;
		}

		public bool NoEmersionControllerAnswer {
			get { return _noEmersionControllerAnswer; }
		}

		public bool SensorOneWire1Error {
			get { return _sensorOneWire1Error; }
		}

		public bool SensorOneWire2Error {
			get { return _sensorOneWire2Error; }
		}
	}
}