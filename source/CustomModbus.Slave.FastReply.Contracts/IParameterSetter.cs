using System;

namespace CustomModbus.Slave.FastReply.Contracts {
	public interface IParameterSetter {
		void SetParameterAsync(int zeroBasedParameterNumber, ushort value, Action<Exception> callback);
	}

	public interface IParameterSetter<in TSet> {
		void SetParameterAsync(int zeroBasedParameterNumber, TSet value, Action<Exception> callback);
	}
}