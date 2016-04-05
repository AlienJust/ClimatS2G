using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapReply03 : IMukFlapReply03 {
		private ushort _flapPosition;
		private int _temperatureAddress1;
		private int _temperatureAddress2;
		private IIncomingSignals _incomingSignals;
		private byte _outgoingSignals;
		private ushort _analogInput;
		private MukFlapWorkmodeStage _automaticModeStage;
		private IMukFlapDiagnostic1 _diagnostic1;
		private IMukFlapDiagnostic2 _diagnostic2;
		private IMukFlapDiagnostic3OneWireSensor _diagnostic3OneWire1;
		private IMukFlapDiagnostic3OneWireSensor _diagnostic4OneWire2;
		private IEmersonDiagnostic _emersonDiagnostic;
		private string _emersonTemperature;
		private string _emersonPressure;
		private string _emersonValveSetting;
		private ushort _firmwareBuildNumber;

		public ushort FlapPosition {
			get { return _flapPosition; }
		}

		public int TemperatureAddress1 {
			get { return _temperatureAddress1; }
		}

		public int TemperatureAddress2 {
			get { return _temperatureAddress2; }
		}

		public IIncomingSignals IncomingSignals {
			get { return _incomingSignals; }
		}

		public byte OutgoingSignals {
			get { return _outgoingSignals; }
		}

		public ushort AnalogInput {
			get { return _analogInput; }
		}

		public MukFlapWorkmodeStage AutomaticModeStage {
			get { return _automaticModeStage; }
		}

		public IMukFlapDiagnostic1 Diagnostic1 {
			get { return _diagnostic1; }
		}

		public IMukFlapDiagnostic2 Diagnostic2 {
			get { return _diagnostic2; }
		}

		public IMukFlapDiagnostic3OneWireSensor Diagnostic3OneWire1 {
			get { return _diagnostic3OneWire1; }
		}

		public IMukFlapDiagnostic3OneWireSensor Diagnostic4OneWire2 {
			get { return _diagnostic4OneWire2; }
		}

		public IEmersonDiagnostic EmersonDiagnostic {
			get { return _emersonDiagnostic; }
		}

		public string EmersonTemperature {
			get { return _emersonTemperature; }
		}

		public string EmersonPressure {
			get { return _emersonPressure; }
		}

		public string EmersonValveSetting {
			get { return _emersonValveSetting; }
		}

		public ushort FirmwareBuildNumber {
			get { return _firmwareBuildNumber; }
		}
	}
}