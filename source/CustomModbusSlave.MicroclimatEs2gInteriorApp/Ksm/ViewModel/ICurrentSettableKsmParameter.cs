namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm.ViewModel {
	interface ICurrentSettableKsmParameter {
		void SetCurrentValue(ushort? value);
	}
}