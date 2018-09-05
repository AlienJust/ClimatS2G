namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Reply03.DataModel.Contracts {
	interface IEmersonDiagnostic {
		bool ErrorValve1 { get; }
		bool ErrorValve2 { get; }
		bool ErrorPressureSensor1 { get; }
		bool ErrorPressureSensor2 { get; }
		bool ErrorTemperatureSensor1 { get; }
		bool ErrorTemperatureSensor2 { get; }
		bool ErrorTemperatureSensor3 { get; }
	}
}
