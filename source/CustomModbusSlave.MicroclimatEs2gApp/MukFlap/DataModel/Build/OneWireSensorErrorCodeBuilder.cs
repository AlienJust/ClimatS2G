using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
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