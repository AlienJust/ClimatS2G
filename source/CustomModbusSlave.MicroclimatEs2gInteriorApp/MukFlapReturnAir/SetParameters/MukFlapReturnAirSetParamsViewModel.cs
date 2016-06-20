using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlienJust.Support.Concurrent.Contracts;
using AlienJust.Support.ModelViewViewModel;
using CustomModbusSlave.MicroclimatEs2gApp.Ksm;
using CustomModbusSlave.MicroclimatEs2gApp.SetParams;

namespace CustomModbusSlave.MicroclimatEs2gApp.MukFlapReturnAir.SetParameters {
	class MukFlapReturnAirSetParamsViewModel : ViewModelBase {
		public SettableParameterViewModel SettableParam625 { get; }
		public SettableParameterViewModel SettableParam626 { get; }

		public MukFlapReturnAirSetParamsViewModel(IThreadNotifier uiNotifier, IParameterSetter parameterSetter) {
			SettableParam625 = new SettableParameterViewModel(625, "Ручной/автоматический режим. 1 = ручной режим", 1.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
			SettableParam626 = new SettableParameterViewModel(626, "Уставка ШИМ на заслонку в ручном режиме 0 - 255", 255.0, 0.0, null, "f0", new DoubleUshortConverterSimple(), parameterSetter, uiNotifier);
		}
	}
}
