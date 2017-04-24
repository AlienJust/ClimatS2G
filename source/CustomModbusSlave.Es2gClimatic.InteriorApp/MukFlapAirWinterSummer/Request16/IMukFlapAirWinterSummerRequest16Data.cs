using System.Collections.Generic;
using AlienJust.Support.Collections;
using CustomModbusSlave.Es2gClimatic.Shared;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirWinterSummer.Request16 {

	/// <summary>
	/// Данные запроса от КСМ к МУК заслонки рециркуляционного воздуха
	/// </summary>
	interface IMukFlapAirWinterSummerRequest16Data {
		int MukFlapAirWinterSummerWorkmodeCommandFromKsm { get; }
		int TemperatureOuterAir { get; }
		double TemperatureInnerAir { get; }
		int FanLevelSetting { get; }
		int Reserve725 { get; }
		int Reserve726 { get; }
	}

	class MukFlapAirWinterSummerRequest16DataSimple : IMukFlapAirWinterSummerRequest16Data {
		public int MukFlapAirWinterSummerWorkmodeCommandFromKsm { get; set; }
		public int TemperatureOuterAir { get; set; }
		public double TemperatureInnerAir { get; set; }
		public int FanLevelSetting { get; set; }
		public int Reserve725 { get; set; }
		public int Reserve726 { get; set; }
	}

	class MukFlapAirWinterSummerRequest16DataBuilder : IBuilder<IMukFlapAirWinterSummerRequest16Data> {
		private readonly IList<byte> _bytes;

		public MukFlapAirWinterSummerRequest16DataBuilder(IList<byte> bytes) {
			_bytes = bytes;
		}

		public IMukFlapAirWinterSummerRequest16Data Build() {
			return new MukFlapAirWinterSummerRequest16DataSimple {
				MukFlapAirWinterSummerWorkmodeCommandFromKsm = new BytesPair(_bytes[7], _bytes[8]).HighFirstUnsignedValue,
				TemperatureOuterAir = new BytesPair(_bytes[9], _bytes[10]).HighFirstSignedValue,
				TemperatureInnerAir = new BytesPair(_bytes[11], _bytes[12]).HighFirstSignedValue * 0.01,
				FanLevelSetting = new BytesPair(_bytes[13], _bytes[14]).HighFirstSignedValue,
				Reserve725 = new BytesPair(_bytes[15], _bytes[16]).HighFirstUnsignedValue,
				Reserve726 = new BytesPair(_bytes[17], _bytes[18]).HighFirstUnsignedValue
			};
		}
	}

	class CmdListenerMukFlapAirWinterSummerRequest16 : CmdListenerBase<IMukFlapAirWinterSummerRequest16Data> {
		public CmdListenerMukFlapAirWinterSummerRequest16(byte addrToCheck, byte codeToCheck, int length) : base(addrToCheck, codeToCheck, length) {
		}

		public override IMukFlapAirWinterSummerRequest16Data BuildData(IList<byte> bytes) {
			return new MukFlapAirWinterSummerRequest16DataBuilder(bytes).Build();
		}
	}
}
