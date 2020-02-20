using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data {
	class AutomaticWorkmodeStageSimple : IAutomaticWorkmodeStage {
		public AutomaticWorkmodeStageSimple(ushort valueRaw) {
			ValueRaw = valueRaw;
		}

		public AutomaticWorkmodeStage ValueParsed => new AutomaticStageModeBuilder(ValueRaw).Build();
		public ushort ValueRaw { get; }
	}
}
