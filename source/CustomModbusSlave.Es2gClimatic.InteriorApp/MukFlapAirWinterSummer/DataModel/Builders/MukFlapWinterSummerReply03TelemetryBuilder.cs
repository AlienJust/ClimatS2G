using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Build;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Contracts;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.SimpleReleases;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.Diagnostic2;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapWinterSummer.DataModel.Builders {
	class MukFlapWinterSummerReply03TelemetryBuilder : IBuilder<IMukFlapWinterSummerReply03Telemetry> {
		private readonly IList<byte> _data;
		public MukFlapWinterSummerReply03TelemetryBuilder(IList<byte> data) {
			_data = data;
		}

		public IMukFlapWinterSummerReply03Telemetry Build() {
			var flapPwmSetting = _data[3] * 256 + _data[4];

			var temperatureAddress1 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[5], _data[6]), 0.01, 0.0, new BytesPair(0x85,0x00));
			var temperatureAddress2 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[7], _data[8]), 0.01, 0.0, new BytesPair(0x85, 0x00));

			var incomingSignals = new IncomingSignalsBuilder(_data[10]).Build();
			var outgoingSignals = _data[12];

			var analogInput = (_data[14] + _data[13] * 256) * 0.1; // voltage
			var automaticModeStage = new MukFlapWorkmodeStageBuilder(_data[16] + _data[15]*250).Build();

			var diagnostic1 = new MukFlapDiagnostic1Builder(_data[18] + _data[17]*256).Build();
			var diagnostic2 = new MukFlapDiagnostic2Builder(_data[20] + _data[19]*256).Build();
			var diagnostic3 = new MukFlapDiagnosticOneWireSensorBuilder(_data[22] + _data[21] * 256).Build();
			var diagnostic4 = new MukFlapDiagnosticOneWireSensorBuilder(_data[24] + _data[23] * 256).Build();

			var reserve11 = _data[26] + _data[25]*256;
			var reserve12 = _data[28] + _data[27] * 256;
			var reserve13 = _data[30] + _data[29] * 256;
			var reserve14 = _data[32] + _data[31] * 256;
			var reserve15 = _data[34] + _data[33] * 256;
			var reserve16 = _data[36] + _data[35] * 256;

			var reserve17 = _data[38] + _data[37] * 256;
			var reserve18 = _data[40] + _data[39] * 256;
			var firmwareBuildNumber = _data[42] + _data[41] * 256;
			var reserve20 = _data[44] + _data[43] * 256;

			return new MukFlapWinterSummerReply03Telemetry(
				flapPwmSetting, temperatureAddress1, temperatureAddress2,
				incomingSignals,
				outgoingSignals,
				analogInput,
				automaticModeStage,
				diagnostic1,
				diagnostic2,
				diagnostic3,
				diagnostic4,

				reserve11,
				reserve12,
				reserve13,

				reserve14,
				reserve15,

				reserve16,

				reserve17,
				reserve18,
				firmwareBuildNumber,
				reserve20);
		}
	}
}
