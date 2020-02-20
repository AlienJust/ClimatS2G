﻿using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.Converters {
	static class MukFlapOuterAirWorkmodeStageExtensions {
		public static string ToText(this MukFlapOuterAirWorkmodeStage ns) {
			switch (ns) {
				case MukFlapOuterAirWorkmodeStage.ControllerInitialization:
					return "Инициализация контроллера";
				case MukFlapOuterAirWorkmodeStage.FlapTesting:
					return "Тест заслонки";
				case MukFlapOuterAirWorkmodeStage.WorkMode:
					return "Рабочий режим";
				case MukFlapOuterAirWorkmodeStage.WorkModeWithFailedFlap:
					return "Рабочий режим с неисправной заслонкой";
				case MukFlapOuterAirWorkmodeStage.WorkModePwmCorrectionBack:
					return "Корректировка ШИМ на заслонку по обратной связи, рабочий режим";
				case MukFlapOuterAirWorkmodeStage.NoKsm:
					return "Режим работы при отсутсвии КСМ";
				case MukFlapOuterAirWorkmodeStage.SetFromRemoteController:
					return "Режим работы при уставке с пульта";
				case MukFlapOuterAirWorkmodeStage.FlapCloseInWashingMode:
					return "Закрытие заслонки в режиме мойки";
				case MukFlapOuterAirWorkmodeStage.Unknown:
					return "Неизвестное значение";
				default:
					return ns.ToString();
			}
		}
	}
}