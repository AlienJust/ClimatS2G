namespace CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire {
	class OneWireSensorErrorCodeBuilder : IBuilder<IOneWireSensorErrorCode> {
		private readonly int _absoluteValue;

		public OneWireSensorErrorCodeBuilder(int absoluteValue) {
			_absoluteValue = absoluteValue;
		}

		public IOneWireSensorErrorCode Build() {
			return new OneWireSensorErrorCodeSimple(_absoluteValue);
		}
	}
}