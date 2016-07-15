using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts {
	public interface IParameterSetter {
		void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback);
	}
}