using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukAirExhauster.Data.Contracts {
	interface IReply03Data {
		int HeatPwm { get; }
		int TemperatureOneWire { get; }
		int InputSignals { get; }
		int OutputSignals { get; }
		double AnalogInputCo2 { get; }
		IAutomaticWorkmodeStage WorkmodeStage { get; }
		int Diagnostic1 { get; }
		int Diagnostic2Fan { get; }
		int Diagnostic3OneWire { get; }
		int FirmwareBuildNumber { get; }
		int Reserve11 { get; }
		int Reserve12 { get; }
	}

	internal interface IAutomaticWorkmodeStage {
		AutomaticWorkmodeStage ValueParsed { get; }
		ushort ValueRaw { get; }
	}

	internal enum AutomaticWorkmodeStage {
		ControllerInitialization, // 0
		WaitingForPowerOnCommand, // 1
		WorkingWithFanOnByTable, // 2
		Unknown
	}

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

	class AutomaticStageModeBuilder : IBuilder<AutomaticWorkmodeStage> {
		private readonly ushort _value;
		public AutomaticStageModeBuilder(ushort value) {
			_value = value;
		}

		public AutomaticWorkmodeStage Build() {
			switch (_value) {
				case 0:
					return AutomaticWorkmodeStage.ControllerInitialization;
				case 1:
					return AutomaticWorkmodeStage.WaitingForPowerOnCommand;
				case 2:
					return AutomaticWorkmodeStage.WorkingWithFanOnByTable;
				default:
					return AutomaticWorkmodeStage.Unknown;
			}
		}
	}
}
