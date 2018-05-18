using System;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data.Contracts;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFanAirExhauster.Data {
	static class AutomaticStageModeExtensions {
		public static string ToText(this AutomaticWorkmodeStage value) {
			switch (value) {
				case AutomaticWorkmodeStage.ControllerInitialization:
					return "инициализация контроллера";
				case AutomaticWorkmodeStage.WaitingForPowerOnCommand:
					return "ожидание команды на включение";
				case AutomaticWorkmodeStage.WorkingWithFanOnByTable:
					return "работа с включением вентилятора по таблице";
				case AutomaticWorkmodeStage.Unknown:
					return "неизвестно";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), "неизвестное значение");
			}
		}
	}
}