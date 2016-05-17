namespace CustomModbusSlave.MicroclimatEs2gApp.Ksm {
	internal interface IKsmWritableParameterViewModel {
		string Name { get; }
		double ValueToSet { get; set; }
		double ReceivedValue { get; }
	}
}