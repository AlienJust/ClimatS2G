namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts {
	internal interface IAutomaticWorkmodeStage {
		AutomaticWorkmodeStage ValueParsed { get; }
		ushort ValueRaw { get; }
	}
}