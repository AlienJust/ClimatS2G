using AlienJust.Support.Conversion.Contracts;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukWarmFloor.Reply03
{
	internal sealed class MukWarmFloorAutomaticModeStageFromIntBuilder : IBuilderOneToOne<int, MukWarmFloorAutomaticModeStage>
	{
		public MukWarmFloorAutomaticModeStage Build(int source)
		{
			switch (source)
			{
				case 0:
					return MukWarmFloorAutomaticModeStage.ControllerInitialization;
				case 1:
					return MukWarmFloorAutomaticModeStage.HeatModeIsOff;
				case 2:
					return MukWarmFloorAutomaticModeStage.HeatModeIsOn;
				default:
					return MukWarmFloorAutomaticModeStage.Unknown;
			}
		}
	}
}
