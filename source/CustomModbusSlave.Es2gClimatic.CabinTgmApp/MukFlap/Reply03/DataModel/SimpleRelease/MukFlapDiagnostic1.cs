using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.SimpleRelease {
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
