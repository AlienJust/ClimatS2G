namespace CustomModbusSlave.MicroclimatEs2gApp.Common.SetParamsAndKsm.Contracts {
	public interface IParameterRoot {
		void NotifyBitValueChanged(int zbBitNumber, bool value);
	}
}