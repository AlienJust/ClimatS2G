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
		public SettableParameterViewModel SettableParam727 { get; }
		public SettableParameterViewModel SettableParam728 { get; }
		public SettableParameterViewModel SettableParam729 { get; }
		public SettableParameterViewModel SettableParam730 { get; }
		public SettableParameterViewModel SettableParam731 { get; }
		public SettableParameterViewModel SettableParam732 { get; }
		public SettableParameterViewModel SettableParam733 { get; }
		public SettableParameterViewModel SettableParam734 { get; }
		public SettableParameterViewModel SettableParam735 { get; }

		public MukFlapWinterSummerSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam727 = new SettableParameterViewModel(727, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam728 = new SettableParameterViewModel(728, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam729 = new SettableParameterViewModel(729, "H – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam730 = new SettableParameterViewModel(730, "I – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam731 = new SettableParameterViewModel(731, "J – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam732 = new SettableParameterViewModel(732, "A – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam733 = new SettableParameterViewModel(733, "E – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam734 = new SettableParameterViewModel(734, "C – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam735 = new SettableParameterViewModel(735, "D – параметр драйвера 1w", 65535.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);

		}
	}
}
