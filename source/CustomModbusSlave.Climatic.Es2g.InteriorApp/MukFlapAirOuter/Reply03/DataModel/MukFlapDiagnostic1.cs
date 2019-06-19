using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel {
	class MukFlapDiagnostic1 : IMukFlapDiagnostic1 {
		public bool NoEmersionControllerAnswer { get; set; }
		public bool SensorOneWire1Error { get; set; }
		public bool SensorOneWire2Error { get; set; }
	}
}
