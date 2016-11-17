namespace CustomModbusSlave.Es2gClimatic.Shared.SetParamsAndKsm.Contracts {
	public interface IParameterRoot {
		void NotifyBitValueChanged(int zbBitNumber, bool value);
	}
}