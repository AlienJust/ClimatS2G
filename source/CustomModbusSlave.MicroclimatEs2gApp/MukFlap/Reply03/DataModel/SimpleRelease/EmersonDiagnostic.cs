using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class EmersonDiagnostic : IEmersonDiagnostic {
		private readonly bool _errorValve1; 
		private readonly bool _errorValve2;
		private readonly bool _errorPressureSensor1;
		private readonly bool _errorPressureSensor2;
		private readonly bool _errorTemperatureSensor1;
		private readonly bool _errorTemperatureSensor2;
		private readonly bool _errorTemperatureSensor3;


		public EmersonDiagnostic(bool errorValve1, bool errorValve2, bool errorPressureSensor1, bool errorPressureSensor2, bool errorTemperatureSensor1, bool errorTemperatureSensor2, bool errorTemperatureSensor3) {
			_errorValve1 = errorValve1;
			_errorValve2 = errorValve2;
			_errorPressureSensor1 = errorPressureSensor1;
			_errorPressureSensor2 = errorPressureSensor2;
			_errorTemperatureSensor1 = errorTemperatureSensor1;
			_errorTemperatureSensor2 = errorTemperatureSensor2;
			_errorTemperatureSensor3 = errorTemperatureSensor3;
			
		}

		public bool ErrorValve1 {
			get { return _errorValve1; }
		}

		public bool ErrorValve2 {
			get { return _errorValve2; }
		}

		public bool ErrorPressureSensor1 {
			get { return _errorPressureSensor1; }
		}

		public bool ErrorPressureSensor2 {
			get { return _errorPressureSensor2; }
		}

		public bool ErrorTemperatureSensor1 {
			get { return _errorTemperatureSensor1; }
		}

		public bool ErrorTemperatureSensor2 {
			get { return _errorTemperatureSensor2; }
		}

		public bool ErrorTemperatureSensor3 {
			get { return _errorTemperatureSensor3; }
		}
	}
}