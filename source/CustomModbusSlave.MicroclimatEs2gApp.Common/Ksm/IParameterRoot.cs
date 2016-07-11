namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	public interface IParameterRoot {
		void NotifyBitValueChanged(int zbBitNumber, bool value);
	}
}