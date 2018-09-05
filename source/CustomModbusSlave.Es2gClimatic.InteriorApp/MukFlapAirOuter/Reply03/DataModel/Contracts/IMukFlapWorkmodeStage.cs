namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts {
	internal interface IMukFlapWorkmodeStage {
		int AbsoluteValue { get; }
		MukFlapOuterAirWorkmodeStage KnownValue { get; }
	}
}
