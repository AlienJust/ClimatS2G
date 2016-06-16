﻿using System.Collections.Generic;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts {
	class Reply03DataBuilder : IBuilder<IReply03Data> {
		private readonly IList<byte> _bytes;
		public Reply03DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IReply03Data Build() {


			return new Reply03DataSimple(
				_bytes[3]*256 + _bytes[4],
				_bytes[5]*256 + _bytes[6],
				_bytes[7]*256 + _bytes[8],
				_bytes[9]*256 + _bytes[10],
				(_bytes[11]*256 + _bytes[12])*0.1,
				new AutomaticWorkmodeStageSimple((ushort) (_bytes[13]*256 + _bytes[14])),
				_bytes[15]*256 + _bytes[16],
				_bytes[17]*256 + _bytes[18],
				_bytes[19]*256 + _bytes[20],
				_bytes[21]*256 + _bytes[22],
				_bytes[23]*256 + _bytes[24],
				_bytes[25]*256 + _bytes[26],
				_bytes[27]*256 + _bytes[28]
				);
		}
	}
}