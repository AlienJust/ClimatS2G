using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease {
	class MukFlapDiagnostic1 : IMukFlapDiagnostic1 {
		public MukFlapDiagnostic1(bool noEmersionControllerAnswer, bool sensorOneWire1Error, bool sensorOneWire2Error) {
			NoEmersionControllerAnswer = noEmersionControllerAnswer;
			SensorOneWire1Error = sensorOneWire1Error;
			SensorOneWire2Error = sensorOneWire2Error;
		}

		public bool NoEmersionControllerAnswer { get; }

		public bool SensorOneWire1Error { get; }

		public bool SensorOneWire2Error { get; }
	}
}