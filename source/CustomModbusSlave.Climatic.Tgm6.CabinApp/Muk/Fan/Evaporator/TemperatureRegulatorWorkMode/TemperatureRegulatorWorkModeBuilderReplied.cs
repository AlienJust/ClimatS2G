using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.CabinTgmApp.Muk.Fan.Evaporator.TemperatureRegulatorWorkMode
{
	public class TemperatureRegulatorWorkModeBuilderReplied : IBuilder<ITemperatureRegulatorWorkMode>
	{
		private readonly BytesPair _dataBytes;

		public TemperatureRegulatorWorkModeBuilderReplied(BytesPair dataBytes)
		{
			_dataBytes = dataBytes;
		}

		public ITemperatureRegulatorWorkMode Build()
		{
			int val = _dataBytes.HighFirstUnsignedValue;

			bool cool = (val & 0x04) != 0x00;
			bool restrict = (val & 0x20) != 0x00;

			return new TemperatureRegulatorWorkModeSimple(val, cool, restrict);
		}
	}
}
