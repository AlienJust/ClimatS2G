using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Build;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.Diagnostic2;
using CustomModbusSlave.Es2gClimatic.Shared.MukFlap.DiagnosticOneWire;
using CustomModbusSlave.Es2gClimatic.Shared.SensorIndications;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirRecycle.Reply03 {
	class MukFlapReturnAirReply03TelemetryBuilder : IBuilder<IMukFlapReturnAirReply03Telemetry> {
		private readonly IList<byte> _data;
		public MukFlapReturnAirReply03TelemetryBuilder(IList<byte> data) {
			_data = data;
		}

		public IMukFlapReturnAirReply03Telemetry Build() {
			var flapPwmSetting = _data[3] * 256 + _data[4];

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

			var concentratorState = _data[26] + _data[25]*256;
			var concentratorDrivers = _data[28] + _data[27] * 256;
			var concentratorVoltage = _data[30] + _data[29] * 256;
			var reserve14 = _data[32] + _data[31] * 256;
			var reserve15 = _data[34] + _data[33] * 256;
			var firmwareBuildNumber = _data[36] + _data[35] * 256;

			var reserve17 = _data[38] + _data[37] * 256;
			var reserve18 = _data[40] + _data[39] * 256;


			return new MukFlapReturnAirReply03Telemetry(
				flapPwmSetting, temperatureAddress1, temperatureAddress2,
				incomingSignals,
				outgoingSignals,
				analogInput,
				automaticModeStage,
				diagnostic1,
				diagnostic2,
				diagnostic3,
				diagnostic4,

				concentratorState,
				new MukFlapReturnAirConcentratorStatusBuilderFromByte(_data[26]).Build(),

				concentratorDrivers,
				concentratorVoltage,
				
				reserve14,
				reserve15,
				
				firmwareBuildNumber,
				
				reserve17,
				reserve18);
		}
	}
}