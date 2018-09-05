﻿using System;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts.Enums {
	enum MukFlapOuterAirWorkmodeStage {
		ControllerInitialization, // 0
		FlapTesting, // 1
		WorkMode, // 2
		WorkModeWithFailedFlap, // 3
		WorkModePwmCorrectionBack, // 4
		NoKsm, // 5
		SetFromRemoteController, // 6
		FlapCloseInWashingMode, // 7
		Unknown // any other value
	}

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
