namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanEvaporator.Reply03 {
	internal class MukFanVaporizerDataReply03Diagnostic1Simple : IMukFanVaporizerDataReply03Diagnostic1 {
		public bool FanControllerLinkLost { get; set; }
		public bool FanErrorByDiscreeteInput { get; set; }
		public bool ErrorOneWireSensor1 { get; set; }
		public bool ErrorOneWireSensor2 { get; set; }
	}
}