using CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts.Enums;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlap.DataModel.Contracts {
	internal interface IMukFlapWorkmodeStage {
		int AbsoluteValue { get; }
		MukFlapOuterAirWorkmodeStage KnownValue { get; }
	}
}