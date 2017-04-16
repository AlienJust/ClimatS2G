using System.Collections.Generic;
using AlienJust.Support.Collections;
using AlienJust.Support.Numeric.Bits;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFridge {
	class MukFridgeFanReply03DataBuilder : IBuilder<IMukFridgeFanReply03Data> {
		private static readonly BytesPair NoSensor = new BytesPair(0x85, 0x00);
		private readonly IList<byte> _data;
		public MukFridgeFanReply03DataBuilder(IList<byte> bytes) {
			_data = bytes;
		}

		public IMukFridgeFanReply03Data Build() {
			return new MukFridgeFanReply03DataSimple {
				FanPwm = new BytesPair(_data[3], _data[4]).HighFirstUnsignedValue,
				CondensingPressure = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[5], _data[6]), 0.1, 0.0, NoSensor),
				IncomingSignals = _data[8],
				OutgoingSignals = _data[10],
				AnalogInput = new BytesPair(_data[11], _data[12]).HighFirstUnsignedValue,
				AutomaticModeStage = new BytesPair(_data[13], _data[14]).HighFirstUnsignedValue,
				WorkMode = new BytesPair(_data[15], _data[16]).HighFirstUnsignedValue,
				Diagnostic1 = new BytesPair(_data[17], _data[18]).HighFirstUnsignedValue,
				Diagnostic2 = new BytesPair(_data[19], _data[20]).HighFirstUnsignedValue,
				FanSpeed = new BytesPair(_data[21], _data[22]).HighFirstUnsignedValue,
				FirmwareBuildNumber = new BytesPair(_data[23], _data[24]).HighFirstUnsignedValue,
				Stage1IsOn = _data[10].GetBit(0),
				Stage2IsOn = _data[10].GetBit(1)
			};
		}
	}
}