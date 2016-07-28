using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Contracts.Enums;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.Reply03.DataModel.Contracts {
	internal interface IMukFlapWorkmodeStage {
		int AbsoluteValue { get; }
		MukFlapWorkmodeStage KnownValue { get; }
	}
}