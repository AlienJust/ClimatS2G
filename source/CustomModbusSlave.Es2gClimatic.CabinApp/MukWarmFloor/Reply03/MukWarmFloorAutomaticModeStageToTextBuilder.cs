using AlienJust.Support.Conversion.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03
{
	internal sealed class MukWarmFloorAutomaticModeStageToTextBuilder : IBuilderOneToOne<MukWarmFloorAutomaticModeStage, string>
	{
		public string Build(MukWarmFloorAutomaticModeStage source)
		{
			switch (source)
			{
				case MukWarmFloorAutomaticModeStage.ControllerInitialization:
					return "Инициализация контроллера";
				case MukWarmFloorAutomaticModeStage.HeatModeIsOff:
					return "Режим обогрева выключен";
				case MukWarmFloorAutomaticModeStage.HeatModeIsOn:
					return "Режим обогрева включен";
				case MukWarmFloorAutomaticModeStage.Unknown:
					return "Неизвестный режим";
				default:
					return "Не реализовано - обратитесь к разработчику";
			}
		}
	}
}