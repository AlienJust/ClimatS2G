namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	class MukFlapAirRecycleDiagnostic1 : IMukFlapAirRecycleDiagnostic1 {
		public bool HighVoltageKeyDriverLinkError { get; set; }
		public bool SensorOneWire1Error { get; set; }
		public bool SensorOneWire2Error { get; set; }
	}
}