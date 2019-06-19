using System;

namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.TextFormatters {
	static class KsmWorkStageExtensions {
		public static string ToText(this KsmWorkStage workStage) {
			switch (workStage) {
				case KsmWorkStage.TurnOff0FormingTurnOffCommandMuk:
					return "Выключение: Формирование команд «выключение» на МУК";
				case KsmWorkStage.TurnOff1Time5SecondsOut:
					return "Выключение: Выдержка паузы 5 секунд, контроль на выключено";
				case KsmWorkStage.TurnOff2:
					return "Выключение: ";
				case KsmWorkStage.TurnOff3:
					return "Выключение: ";
				case KsmWorkStage.TurnOff4:
					return "Выключение: ";

				case KsmWorkStage.TurnOn5FormingTurnOnCommandMuk:
					return "Включение: Формирование команд «включение» на МУК";
				case KsmWorkStage.TurnOn6Time100SecondsOutAndMukTestsControl:
					return "Включение: Выдержка паузы 100 секунд, контроль результатов тестов на МУК";
				case KsmWorkStage.TurnOn7RegulatorOnAtEvaporatorGoingToCoolOrHeatMode:
					return "Включение: Включение регулятора на испарителе, переход в режим «охлаждение» или «обогрев»";
				case KsmWorkStage.TurnOn8CoolModeEmersionWorkControlAndSelfRegulator:
					return "Включение: Режим охлаждение, контроль работоспособности Emerson, собственный регулятор";
				case KsmWorkStage.TurnOn9:
					return "Включение: ";

				case KsmWorkStage.Reserve10:
					return "Резерв: ";
				case KsmWorkStage.Reserve11:
					return "Резерв: ";
				case KsmWorkStage.Reserve12:
					return "Резерв: ";
				case KsmWorkStage.Reserve13:
					return "Резерв: ";
				case KsmWorkStage.Reserve14:
					return "Резерв: ";

				case KsmWorkStage.Settling15:
					return "Отстой: ";
				case KsmWorkStage.Settling16:
					return "Отстой: ";
				case KsmWorkStage.Settling17:
					return "Отстой: ";
				case KsmWorkStage.Settling18:
					return "Отстой: ";
				case KsmWorkStage.Settling19:
					return "Отстой: ";

				case KsmWorkStage.Service20FormingTurnOnCommandMuk:
					return "Сервис: Формирование команд «включение» на МУК";
				case KsmWorkStage.Service21Time100SecondsAndMukTestsControl:
					return "Сервис: Выдержка паузы 100 секунд, контроль результатов тестов на МУК";
				case KsmWorkStage.Service22RegulatorOnAtEvaporatorGoingToCoolOrHeatMode:
					return "Сервис: Включение регулятора на испарителе, переход в режим «охлаждение» или «обогрев»";
				case KsmWorkStage.Service23CoolModeEmersionWorkControlAndSelfRegulator:
					return "Сервис: Режим охлаждение, контроль работоспособности Emerson, собственный регулятор";
				case KsmWorkStage.Service24:
					return "Сервис: ";

				case KsmWorkStage.Washing25FormingTurnOffCommandMuk:
					return "Мойка: Формирование команд «выключение» на МУК (в т.ч. закрытие заслонки)";
				case KsmWorkStage.Washing26Time100SecondsOutAndContolFlapCloseAlsoBvsSignalsControl:
					return "Мойка: Выдержка паузы 100 секунд, контроль на закрытие заслонки, контроль сигналов БВС";
				case KsmWorkStage.Washing27:
					return "Мойка: ";
				case KsmWorkStage.Washing28:
					return "Мойка: ";
				case KsmWorkStage.Washing29:
					return "Мойка: ";

				case KsmWorkStage.ManualMode30FormingTurnOnCommandMuk:
					return "Ручной режим: Формирование команд «включение» на МУК";
				case KsmWorkStage.ManualMode31Time100SecondsOutAndMukTestsControl:
					return "Ручной режим: Выдержка паузы 100 секунд, контроль результатов тестов на МУК";
				case KsmWorkStage.ManualMode32RegulatorOnAtEvaporatorGoingToCoolOrHeatMode:
					return "Ручной режим: Включение регулятора на испарителе, переход в режим «охлаждение» или «обогрев»";
				case KsmWorkStage.ManualMode33CoolModeEmersionWorkControlAndSelfRegulator:
					return "Ручной режим: Режим охлаждение, контроль работоспособности Emerson, собственный регулятор";
				case KsmWorkStage.ManualMode34:
					return "Ручной режим: ";
				default:
					return "ХЗ";
			}
			throw new Exception("Какая-то хрень, исполнение программы в этом месте кода не предусмотрено");
		}
	}
}