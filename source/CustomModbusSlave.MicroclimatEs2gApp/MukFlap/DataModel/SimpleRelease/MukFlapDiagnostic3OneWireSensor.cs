using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapDiagnostic3OneWireSensor : IMukFlapDiagnostic3OneWireSensor {
		private readonly bool _oneWireSensorError;
		private readonly OneWireSensorErrorCode _errorCode;
		public MukFlapDiagnostic3OneWireSensor(bool oneWireSensorError, OneWireSensorErrorCode errorCode) {
			_oneWireSensorError = oneWireSensorError;
			_errorCode = errorCode;
		}

		public bool OneWireSensorError {
			get { return _oneWireSensorError; }
		}

		public OneWireSensorErrorCode ErrorCode {
			get { return _errorCode; }
		}
	}
}