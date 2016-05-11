using System.Collections.Generic;
using CustomModbusSlave.MicroclimatEs2gApp.Common.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts;
using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.SimpleRelease;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Build {
	class MukFlapReply03TelemetryBuilder : IBuilder<IMukFlapReply03Telemetry> {
		private readonly IList<byte> _data;
		public MukFlapReply03TelemetryBuilder(IList<byte> data) {
			_data = data;
		}

		public IMukFlapReply03Telemetry Build() {
			var flapPosition = _data[3] * 256 + _data[4];

			var temperatureAddress1 = (_data[6] + _data[5] * 256) * 0.01;
			var temperatureAddress2 = (_data[8] + _data[7] * 256) * 0.01;

			var incomingSignals = new IncomingSignalsBuilder(_data[10]).Build();
			var outgoingSignals = _data[12];

			var analogInput = (_data[14] + _data[13] * 256) * 0.1; // voltage
			var automaticModeStage = new MukFlapWorkmodeStageBuilder(_data[16] + _data[15]*250).Build();

			var diagnostic1 = new MukFlapDiagnostic1Builder(_data[18] + _data[17]*256).Build();
			var diagnostic2 = new MukFlapOuterAirDiagnostic2Builder(_data[20] + _data[19]*256).Build();
			var diagnostic3 = new MukFlapDiagnosticOneWireSensorBuilder(_data[22] + _data[21] * 256).Build();
			var diagnostic4 = new MukFlapDiagnosticOneWireSensorBuilder(_data[24] + _data[23] * 256).Build();

			var emersonDiagnostic = new EmersonDiagnosticCircuit1Builder(_data[26] + _data[25]*256).Build();

			var emersonTemperature = (_data[28] + _data[27] * 256) * 0.01;
			var emersonPressure = (_data[30] + _data[29] * 256) * 0.1;
			var emersonValveSetting = _data[32] + _data[31] * 256;


			var emersionDiagnostic2 = new EmersonDiagnosticCircuit2Builder(_data[34] + _data[33] * 256).Build();
			var emersonTemperature2 = (_data[36] + _data[35] * 256) * 0.01;
			var emersonPressure2 = (_data[38] + _data[37] * 256) * 0.1;
			var emersonValveSetting2 = _data[40] + _data[39] * 256;


			var firmwareBuildNumber = _data[42] + _data[41] * 256;

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
				
				emersionDiagnostic2,
				emersonTemperature2,
				emersonPressure2,
				emersonValveSetting2,
				firmwareBuildNumber
				);
		}
	}
}