namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	internal interface IMukFlapWorkmodeStage {
		int AbsoluteValue { get; set; }
		MukFlapWorkmodeStage KnownValue { get; set; }
	}
}