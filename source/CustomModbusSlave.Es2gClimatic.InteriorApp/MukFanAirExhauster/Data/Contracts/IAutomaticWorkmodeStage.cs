namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukAirExhauster.Data.Contracts {
	internal interface IAutomaticWorkmodeStage {
		AutomaticWorkmodeStage ValueParsed { get; }
		ushort ValueRaw { get; }
	}
}