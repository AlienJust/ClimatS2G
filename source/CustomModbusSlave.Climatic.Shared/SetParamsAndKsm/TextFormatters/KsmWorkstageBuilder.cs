namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	class KsmWorkstageBuilder : IBuilder<KsmWorkStage> {
		private readonly ushort _value;
		public KsmWorkstageBuilder(ushort value) {
			_value = value;
		}

		public KsmWorkStage Build() {
			switch (_value) {
				case 0:
					return KsmWorkStage.TurnOff0FormingTurnOffCommandMuk;
				case 1:
					return KsmWorkStage.TurnOff1Time5SecondsOut;
				case 2:
					return KsmWorkStage.TurnOff2;
				case 3:
					return KsmWorkStage.TurnOff3;
				case 4:
					return KsmWorkStage.TurnOff4;

				case 5:
					return KsmWorkStage.TurnOn5FormingTurnOnCommandMuk;
				case 6:
					return KsmWorkStage.TurnOn6Time100SecondsOutAndMukTestsControl;
				case 7:
					return KsmWorkStage.TurnOn7RegulatorOnAtEvaporatorGoingToCoolOrHeatMode;
				case 8:
					return KsmWorkStage.TurnOn8CoolModeEmersionWorkControlAndSelfRegulator;
				case 9:
					return KsmWorkStage.TurnOn9;

				case 10:
					return KsmWorkStage.Reserve10;
				case 11:
					return KsmWorkStage.Reserve11;
				case 12:
					return KsmWorkStage.Reserve12;
				case 13:
					return KsmWorkStage.Reserve13;
				case 14:
					return KsmWorkStage.Reserve14;

				case 15:
					return KsmWorkStage.Settling15;
				case 16:
					return KsmWorkStage.Settling16;
				case 17:
					return KsmWorkStage.Settling17;
				case 18:
					return KsmWorkStage.Settling18;
				case 19:
					return KsmWorkStage.Settling19;


				case 20:
					return KsmWorkStage.Service20FormingTurnOnCommandMuk;
				case 21:
					return KsmWorkStage.Service21Time100SecondsAndMukTestsControl;
				case 22:
					return KsmWorkStage.Service22RegulatorOnAtEvaporatorGoingToCoolOrHeatMode;
				case 23:
					return KsmWorkStage.Service23CoolModeEmersionWorkControlAndSelfRegulator;
				case 24:
					return KsmWorkStage.Service24;

				case 25:
					return KsmWorkStage.Washing25FormingTurnOffCommandMuk;
				case 26:
					return KsmWorkStage.Washing26Time100SecondsOutAndContolFlapCloseAlsoBvsSignalsControl;
				case 27:
					return KsmWorkStage.Washing27;
				case 28:
					return KsmWorkStage.Washing28;
				case 29:
					return KsmWorkStage.Washing29;

				case 30:
					return KsmWorkStage.ManualMode30FormingTurnOnCommandMuk;
				case 31:
					return KsmWorkStage.ManualMode31Time100SecondsOutAndMukTestsControl;
				case 32:
					return KsmWorkStage.ManualMode32RegulatorOnAtEvaporatorGoingToCoolOrHeatMode;
				case 33:
					return KsmWorkStage.ManualMode33CoolModeEmersionWorkControlAndSelfRegulator;
				case 34:
					return KsmWorkStage.ManualMode34;

				default:
					return KsmWorkStage.Unknown;
			}
		}
	}
}
