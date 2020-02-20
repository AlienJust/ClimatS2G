using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Request16 {
	internal sealed class ControlOfHiVoltageContactorBuilder : IBuilder<IControlOfHiVoltageContactor> {
		private readonly byte _data;
		public ControlOfHiVoltageContactorBuilder(byte data) {
			_data = data;
		}

		public IControlOfHiVoltageContactor Build() {
			return new ControlOfHiVoltageContactor(_data.GetBit(0), _data.GetBit(1), _data.GetBit(2), _data.GetBit(3));
		}
	}
}
