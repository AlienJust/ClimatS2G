namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts {
	class AutomaticWorkmodeStageSimple : IAutomaticWorkmodeStage {
		public AutomaticWorkmodeStageSimple(ushort valueRaw) {
			ValueRaw = valueRaw;
		}

		public AutomaticWorkmodeStage ValueParsed => new AutomaticStageModeBuilder(ValueRaw).Build();
		public ushort ValueRaw { get; }
	}
}