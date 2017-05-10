namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	class MukFlapReturnAirConcentratorStatusSimple : IMukFlapReturnAirConcentratorStatus {
		public bool CommandToPal { get; set; }
		public bool CommandFromPal { get; set; }
		public bool WorkOrError { get; set; }
		public bool ErrorNoAnswerFromDriver { get; set; }
		public bool ErrorByCurrentCc { get; set; }
		public bool ComparatorValue { get; set; }
		public bool Reserve { get; set; }
		public bool Address { get; set; }
	}
}