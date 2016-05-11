using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface IParameterSetter {
		void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback);
	}
}