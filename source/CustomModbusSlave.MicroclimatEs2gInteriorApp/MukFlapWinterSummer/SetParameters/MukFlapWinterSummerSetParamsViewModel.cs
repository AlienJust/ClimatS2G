using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapWinterSummer.SetParameters {
	class MukFlapWinterSummerSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam827 { get; }
		public SettableParameterViewModel SettableParam828 { get; }
		public SettableParameterViewModel SettableParam829 { get; }
		public SettableParameterViewModel SettableParam830 { get; }
		public SettableParameterViewModel SettableParam831 { get; }
		public SettableParameterViewModel SettableParam832 { get; }
		public SettableParameterViewModel SettableParam833 { get; }
		public SettableParameterViewModel SettableParam834 { get; }
		public SettableParameterViewModel SettableParam835 { get; }

		public MukFlapWinterSummerSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam827 = new SettableParameterViewModel(827, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam828 = new SettableParameterViewModel(828, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam829 = new SettableParameterViewModel(829, "H – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam830 = new SettableParameterViewModel(830, "I – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam831 = new SettableParameterViewModel(831, "J – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam832 = new SettableParameterViewModel(832, "A – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam833 = new SettableParameterViewModel(833, "E – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam834 = new SettableParameterViewModel(834, "C – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam835 = new SettableParameterViewModel(835, "D – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);

		}
	}
}
