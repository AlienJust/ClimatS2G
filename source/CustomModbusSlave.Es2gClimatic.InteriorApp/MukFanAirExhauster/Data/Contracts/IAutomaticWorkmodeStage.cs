namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts {
	internal interface IAutomaticWorkmodeStage {
		AutomaticWorkmodeStage ValueParsed { get; }
		ushort ValueRaw { get; }
	}
}