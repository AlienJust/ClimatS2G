using System;
using System.Windows.Data;
using CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.DataModel.Contracts.Enums;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp.MukFlapOuterAir.Reply03.Converters {
	[ValueConversion(typeof(double), typeof(int))]
	class EnumMukFlapOuterAirWorkmodeStageToStringConverter : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			var ns = (MukFlapOuterAirWorkmodeStage)value; // TODO: might throw exception?

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

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			// TODO: if needed
			throw new NotImplementedException("implement if needed");
		}

		#endregion
	}
}
