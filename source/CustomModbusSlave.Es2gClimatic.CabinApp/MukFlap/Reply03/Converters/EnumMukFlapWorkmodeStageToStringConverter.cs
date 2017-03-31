using System;
using System.Windows.Data;
using CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts.Enums;

namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.Converters {
	[ValueConversion(typeof(double), typeof(int))]
	class EnumMukFlapWorkmodeStageToStringConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var ns = (MukFlapWorkmodeStage)value; // TODO: might throw exception?

			switch (ns) {
				case MukFlapWorkmodeStage.ControllerInitialization:
					return "Инициализация контроллера";
				case MukFlapWorkmodeStage.FlapTesting:
					return "Тест заслонки";
				case MukFlapWorkmodeStage.WorkMode:
					return "Рабочий режим";
				case MukFlapWorkmodeStage.WorkModeWithFailedFlap:
					return "Рабочий режим с неисправной заслонкой";
				case MukFlapWorkmodeStage.WorkModePwmCorrectionBack:
					return "Корректировка ШИМ на заслонку по обратной связи, рабочий режим";
				case MukFlapWorkmodeStage.NoKsm:
					return "Режим работы при отсутсвии КСМ";
				case MukFlapWorkmodeStage.RemoteControlSettig:
					return "Режим работы по уставке с пульта";
				case MukFlapWorkmodeStage.FlapCloseWhileWashing:
					return "Закрытие заслонки в режиме мойки";

				case MukFlapWorkmodeStage.Unknown:
					return "Неизвестное значение";
				default:
					return ns.ToString();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			// TODO: if needed
			throw new NotImplementedException("implement if needed");
		}

		#endregion
	}
}