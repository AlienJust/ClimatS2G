using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.OneWire;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data {
	class MukAirExhausterReply03DataBuilder : IBuilder<IMukAirExhausterReply03Data> {
		private readonly IList<byte> _bytes;
		public MukAirExhausterReply03DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IMukAirExhausterReply03Data Build() {
			return new MukAirExhausterReply03DataSimple(
				heatPwm: _bytes[3]*256 + _bytes[4],
				temperatureOneWire: new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_bytes[5], _bytes[6]), 1.0, 0.0, new BytesPair(0x85,0x00)),
				inputSignals: _bytes[7]*256 + _bytes[8],
				outputSignals: _bytes[9]*256 + _bytes[10],
				analogInputCo2: (_bytes[11]*256 + _bytes[12]) * 1.0,
				workmodeStage: new AutomaticWorkmodeStageSimple((ushort) (_bytes[13]*256 + _bytes[14])),
				fanSpeed: _bytes[15]*256 + _bytes[16],
				diagnostic1:_bytes[17]*256 + _bytes[18],
				diagnostic2Fan: _bytes[19]*256 + _bytes[20],
				diagnostic3OneWire: _bytes[21]*256 + _bytes[22],
				firmwareBuildNumber: _bytes[23]*256 + _bytes[24],
				reserve11: _bytes[25]*256 + _bytes[26],
				reserve12: _bytes[27]*256 + _bytes[28]
				);
		}
	}
}
