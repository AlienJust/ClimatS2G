using CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data {
	class AutomaticWorkmodeStageSimple : IAutomaticWorkmodeStage {
		public AutomaticWorkmodeStageSimple(ushort valueRaw) {
			ValueRaw = valueRaw;
		}

		public AutomaticWorkmodeStage ValueParsed => new AutomaticStageModeBuilder(ValueRaw).Build();
		public ushort ValueRaw { get; }
	}
}