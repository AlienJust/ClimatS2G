using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukVaporizer.SetParameters {
	class MukVaporizerSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam324 { get; }
		public SettableParameterViewModel SettableParam325 { get; }
		public SettableParameterViewModel SettableParam326 { get; }
		public SettableParameterViewModel SettableParam327 { get; }
		public SettableParameterViewModel SettableParam328 { get; }

		public SettableParameterViewModel SettableParam329 { get; }
		public SettableParameterViewModel SettableParam330 { get; }

		public SettableParameterViewModel SettableParam331 { get; }
		public SettableParameterViewModel SettableParam332 { get; }
		public SettableParameterViewModel SettableParam333 { get; }
		public SettableParameterViewModel SettableParam334 { get; }

		public SettableParameterViewModel SettableParam335 { get; }

		public MukVaporizerSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam324 = new SettableParameterViewModel(324, "Zone зона нечувствительности ПИДа", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam325 = new SettableParameterViewModel(325, "Td дифференциальная составляющая", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam326 = new SettableParameterViewModel(326, "Kp коеффициент пропорциональности", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam327 = new SettableParameterViewModel(327, "T_i интегральная составляющая", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam328 = new SettableParameterViewModel(328, "T_0 время вызова ПИДа", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);

			SettableParam329 = new SettableParameterViewModel(329, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam330 = new SettableParameterViewModel(330, "Уставка уровень вентилир.в ручном режиме 0 - 4", 4.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);

			SettableParam331 = new SettableParameterViewModel(331, "Уровень вентилирования 1 в режиме настройки; 0-255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam332 = new SettableParameterViewModel(332, "Уровень вентилирования 2 в режиме настройки; 0-255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam333 = new SettableParameterViewModel(333, "Уровень вентилирования 3 в режиме настройки; 0-255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam334 = new SettableParameterViewModel(334, "Уровень вентилирования 4 в режиме настройки; 0-255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam335 = new SettableParameterViewModel(335, "Настроечный параметр регулятора отопления", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
		}
	}
}
