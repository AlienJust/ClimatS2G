using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapOuterAir.SetParameters {
	class MukFlapOuterAirSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam227 { get; }
		public SettableParameterViewModel SettableParam228 { get; }

		public MukFlapOuterAirSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam227 = new SettableParameterViewModel(227, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam228 = new SettableParameterViewModel(228, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
		}
	}
}
