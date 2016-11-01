using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DataModel.Builders;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Build {
	class MukFlapReply03TelemetryBuilder : IBuilder<IMukFlapReply03Telemetry> {
		private readonly IList<byte> _data;
		public MukFlapReply03TelemetryBuilder(IList<byte> data) {
			_data = data;
		}

		public IMukFlapReply03Telemetry Build() {
			var flapPosition = _data[3] * 256 + _data[4];

			var temperatureAddress1 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[5], _data[6]), 0.01, 0.0, new BytesPair(0x85, 0x00));
			var temperatureAddress2 = new SensorIndicationDoubleBasedOnBytesPair(new BytesPair(_data[7], _data[8]), 0.01, 0.0, new BytesPair(0x85, 0x00));

			var incomingSignals = new IncomingSignalsBuilder(_data[10]).Build();
			var outgoingSignals = _data[12];

			var analogInput = (_data[14] + _data[13] * 256) * 0.1; // voltage
			var automaticModeStage = new MukFlapWorkmodeStageBuilder(_data[16] + _data[15]*250).Build();

			var diagnostic1 = new MukFlapDiagnostic1Builder(_data[18] + _data[17]*256).Build();
			var diagnostic2 = new MukFlapDiagnostic2Builder(_data[20] + _data[19]*256).Build();
			var diagnostic3 = new MukFlapDiagnosticOneWireSensorBuilder(_data[22] + _data[21] * 256).Build();
			var diagnostic4 = new MukFlapDiagnosticOneWireSensorBuilder(_data[24] + _data[23] * 256).Build();

			var emersonDiagnostic = new EmersonDiagnosticBuilder(_data[26] + _data[25]*256).Build();

			var emersonTemperature = (_data[28] + _data[27] * 256) * 0.01;
			var emersonPressure = (_data[30] + _data[29] * 256) * 0.01;
			var emersonValveSetting = _data[32] + _data[31] * 256;


			var firmwareBuildNumber = _data[34] + _data[33] * 256;
			return new MukFlapReply03Telemetry(
				flapPosition, temperatureAddress1, temperatureAddress2,
				incomingSignals,
				outgoingSignals,
				analogInput,
				automaticModeStage,
				diagnostic1,
				diagnostic2,
				diagnostic3,
				diagnostic4,
				emersonDiagnostic,
				emersonTemperature,
				emersonPressure,
				emersonValveSetting,
				firmwareBuildNumber
				);
		}
	}
}