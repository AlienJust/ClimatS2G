using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.TemperatureRegulatorWorkMode;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.Shared.MukFanVaporizer.Reply03 {
	internal class MukVaporizerFanReply03TelemetryBuilder : IBuilder<IMukVaporizerFanReply03Telemetry> {
		private static readonly BytesPair NoSensor= new BytesPair(0x85, 0x00); 
		private readonly IList<byte> _data;
		public MukVaporizerFanReply03TelemetryBuilder(IList<byte> bytes) {
			_data = bytes;
		}
		public IMukVaporizerFanReply03Telemetry Build() {
			return new MukVaporizerFanReply03TelemetrySimple {
				FanPwm = new BytesPair(_data[3], _data[4]).HighFirstUnsignedValue,

				TemperatureAddress1 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[5], _data[6]), 0.01, 0.0, NoSensor),
				TemperatureAddress2 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[7], _data[8]), 0.01, 0.0, NoSensor),

				IncomingSignals = _data[10],
				OutgoingSignals = _data[12],

				AnalogInput = new BytesPair(_data[13], _data[14]).HighFirstUnsignedValue,
				HeatingPwm = new BytesPair(_data[15], _data[16]).HighFirstUnsignedValue,
				AutomaticModeStage = new BytesPair(_data[17], _data[18]).HighFirstUnsignedValue,

				TemperatureRegulatorWorkMode = new TemperatureRegulatorWorkModeBuilderReplied(new BytesPair(_data[19], _data[20])).Build(),

				CalculatedTemperatureSetting = new BytesPair(_data[21], _data[22]).HighFirstUnsignedValue * 0.01,
				FanSpeed = new BytesPair(_data[23], _data[24]).HighFirstUnsignedValue,

				Diagnostic1 = new BytesPair(_data[25], _data[26]).HighFirstUnsignedValue,
				Diagnostic2 = new BytesPair(_data[27], _data[28]).HighFirstUnsignedValue,
				Diagnostic3 = new BytesPair(_data[29], _data[30]).HighFirstUnsignedValue,
				Diagnostic4 = new BytesPair(_data[31], _data[32]).HighFirstUnsignedValue,
				Diagnostic5 = new BytesPair(_data[33], _data[34]).HighFirstUnsignedValue,

				FirmwareBuildNumber = new BytesPair(_data[35], _data[36]).HighFirstUnsignedValue
			};
		}
	}
}