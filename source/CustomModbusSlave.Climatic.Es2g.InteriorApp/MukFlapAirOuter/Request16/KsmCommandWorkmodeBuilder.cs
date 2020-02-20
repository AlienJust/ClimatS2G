using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Request16 {
	class KsmCommandWorkmodeBuilder : IBuilder<KsmCommandWorkmode> {
		private readonly int _value;
		public KsmCommandWorkmodeBuilder(int value) {
			_value = value;
		}

		public KsmCommandWorkmode Build() {
			switch (_value) {
				case 0:
					return KsmCommandWorkmode.Off;
				case 1:
					return KsmCommandWorkmode.Auto;
				case 2:
					return KsmCommandWorkmode.Manual;
				default:
					return KsmCommandWorkmode.Unknown;
			}
		}
	}
}
