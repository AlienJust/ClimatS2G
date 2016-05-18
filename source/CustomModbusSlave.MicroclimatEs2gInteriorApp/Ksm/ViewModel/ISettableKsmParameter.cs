namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	interface ISettableKsmParameter {
		void SetCurrentValue(ushort? value);
	}
}