using System;

namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	public interface IParameterSetter {
		void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback);
	}
}