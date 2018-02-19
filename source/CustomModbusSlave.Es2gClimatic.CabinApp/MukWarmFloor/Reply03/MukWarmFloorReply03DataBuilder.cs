using System.Collections.Generic;
using AlienJust.Support.Conversion.Contracts;
using CustomModbusSlave.Es2gClimatic.Shared;
using CustomModbusSlave.Es2gClimatic.Shared.TextPresenters;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03 {
	class MukWarmFloorReply03DataBuilder : IBuilder<IMukWarmFloorReply03Data> {
		private readonly IList<byte> data;

		public MukWarmFloorReply03DataBuilder(IList<byte> reply) {
			data = reply;
		}

		public IMukWarmFloorReply03Data Build() {
			var result = new MukWarmFloorReply03Data() {
				PwmHeat = data[3] * 256 + data[4],
				AnalogueInput = data[5] * 256 + data[6],
				TemperatureRegulatorWorkMode = (data[7] * 256 + data[8]) * 0.01,
				ByteOfIncomingSignals = data[10],
				ByteOfOutgoingSignals = data[12],
				AutomaticModeStage =
		}




			AnalogInput = new UshortTextPresenter(data[6], data[5], false).PresentAsText();
			TemperatureRegulatorWorkMode = new DataDoubleTextPresenter(data[8], data[7], 0.01, 0).PresentAsText();

			IncomingSignals = new ByteTextPresenter(data[10], false).PresentAsText();
			OutgoingSignals = new ByteTextPresenter(data[12], false).PresentAsText();

			AutomaticModeStage = new UshortTextPresenter(data[14], data[13], false).PresentAsText();
			CalculatedTemperatureSetting = new DataDoubleTextPresenter(data[16], data[15], 0.01, 2).PresentAsText();

			Diagnostic1 = new UshortTextPresenter(data[18], data[17], true).PresentAsText(); // TODO: show as bits
			Diagnostic2 = new UshortTextPresenter(data[20], data[19], true).PresentAsText(); // TODO: show as bits

			FirmwareBuildNumber = new DataDoubleTextPresenter(data[22], data[21], 1.0, 0).PresentAsText();
		}
	}
}