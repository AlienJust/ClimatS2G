using System.Collections.Generic;

namespace CustomModbusSlave.Es2gClimatic.Shared.Bvs {

	public class BvsReply65TelemetryBuilder : IBuilder<IBvsReply65Telemetry> {
		private readonly IList<byte> _data;

		public BvsReply65TelemetryBuilder(IList<byte> data) {
			_data = data;
		}

		public IBvsReply65Telemetry Build() {
			return new BvsReply65TelemetrySimple {
				BvsInput1 = (_data[2] & 0x01) == 0x01,
				BvsInput2 = (_data[2] & 0x02) == 0x02,
				BvsInput3 = (_data[2] & 0x04) == 0x04,
				BvsInput4 = (_data[2] & 0x08) == 0x08,
				BvsInput5 = (_data[2] & 0x10) == 0x10,
				BvsInput6 = (_data[2] & 0x20) == 0x20,
				BvsInput7 = (_data[2] & 0x40) == 0x40,
				BvsInput8 = (_data[2] & 0x80) == 0x80,

				BvsInput9 = (_data[3] & 0x01) == 0x01,
				BvsInput10 = (_data[3] & 0x02) == 0x02,
				BvsInput11 = (_data[3] & 0x04) == 0x04,
				BvsInput12 = (_data[3] & 0x08) == 0x08,
				BvsInput13 = (_data[3] & 0x10) == 0x10,
				BvsInput14 = (_data[3] & 0x20) == 0x20,
				BvsInput15 = (_data[3] & 0x40) == 0x40,
				BvsInput16 = (_data[3] & 0x80) == 0x80,
			};
		}
	}
}