using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFridge.SetParameters {
	class MukFridgeSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam415 { get; }
		public SettableParameterViewModel SettableParam416 { get; }
		public SettableParameterViewModel SettableParam417 { get; }
		public SettableParameterViewModel SettableParam418 { get; }
		public SettableParameterViewModel SettableParam419 { get; }

		public SettableParameterViewModel SettableParam420 { get; }
		public SettableParameterViewModel SettableParam421 { get; }

		public SettableParameterViewModel SettableParam422 { get; }
		public SettableParameterViewModel SettableParam423 { get; }
		public SettableParameterViewModel SettableParam424 { get; }

		public MukFridgeSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam415 = new SettableParameterViewModel(415, "Zone зона нечувствительности ПИДа", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam416 = new SettableParameterViewModel(416, "Td дифференциальная составляющая", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam417 = new SettableParameterViewModel(417, "Kp коеффициент пропорциональности", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam418 = new SettableParameterViewModel(418, "T_i интегральная составляющая", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam419 = new SettableParameterViewModel(419, "T_0 время вызова ПИДа", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);

			SettableParam420 = new SettableParameterViewModel(420, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam421 = new SettableParameterViewModel(421, "Уставка уровень вентилир.в ручном режиме 0 - 3", 3.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);

			SettableParam422 = new SettableParameterViewModel(422, "Ограничение  максимального ШИМа в режиме настройки", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam423 = new SettableParameterViewModel(423, "Ограничение  максимального ШИМа стоянки в режиме настройки", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam424 = new SettableParameterViewModel(424, "Ограничение  минимального ШИМа", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
		}
	}
}
