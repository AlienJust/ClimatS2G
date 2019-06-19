namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	internal sealed class MukFlapReturnAirOutgoingSignals : IMukFlapReturnAirOutgoingSignals {
		public bool TurnContactor1On { get; }
		public bool TurnContactor2On { get; }
		
		public MukFlapReturnAirOutgoingSignals(bool turnContactor1On, bool turnContactor2On) {
			TurnContactor1On = turnContactor1On;
			TurnContactor2On = turnContactor2On;
		}
	}
}