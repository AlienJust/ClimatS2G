namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	interface IParameterRoot {
		void NotifyBitValueChanged(int zbBitNumber, bool value);
	}
}