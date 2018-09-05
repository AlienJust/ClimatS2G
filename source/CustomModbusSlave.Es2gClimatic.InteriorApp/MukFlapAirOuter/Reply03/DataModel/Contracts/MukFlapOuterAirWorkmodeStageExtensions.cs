using System;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapAirOuter.Reply03.DataModel.Contracts {
	static class MukFlapOuterAirWorkmodeStageExtensions {
		public static string ToText(this MukFlapOuterAirWorkmodeStage value) {
			switch (value) {
				case MukFlapOuterAirWorkmodeStage.ControllerInitialization:
					return "Инициализация контроллера";
				case MukFlapOuterAirWorkmodeStage.FlapTesting:
					return "Тестирование заслонки";
				case MukFlapOuterAirWorkmodeStage.WorkMode:
					return "Рабочий режим";
				case MukFlapOuterAirWorkmodeStage.WorkModeWithFailedFlap:
					return "Рабочий режим с неисправной заслонкой";
				case MukFlapOuterAirWorkmodeStage.WorkModePwmCorrectionBack:
					return "Корректировка ШИМ на заслонку по обратной связи, рабочий режим";
				case MukFlapOuterAirWorkmodeStage.NoKsm:
					return "Режим работы при отсутствии обмена с КСМ";
				case MukFlapOuterAirWorkmodeStage.SetFromRemoteController:
					return "Режим работы по уставке с пульта";
				case MukFlapOuterAirWorkmodeStage.FlapCloseInWashingMode:
					return "Закрытие заслонки в режиме мойки";
				case MukFlapOuterAirWorkmodeStage.Unknown:
					return "Неизвестный режим работы";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), value, null);
			}
		}
	}
}