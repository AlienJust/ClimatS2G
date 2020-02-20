using AlienJust.Adaptation.WindowsPresentation.Converters;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts {
	internal interface IMukFlapWorkmodeStage {
		int AbsoluteValue { get; }
		MukFlapOuterAirWorkmodeStage KnownValue { get; }
	}

	static class MukFlapWorkmodeStageExtensions {
		public static string ToText(this IMukFlapWorkmodeStage stage) {
			return stage.AbsoluteValue + " - " + stage.KnownValue.ToText();
		}
	}
}
