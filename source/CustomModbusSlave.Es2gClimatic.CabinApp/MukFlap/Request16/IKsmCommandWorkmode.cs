namespace CustomModbusSlave.Es2gClimatic.CabinApp.MukFlap.Request16 {
	internal interface IKsmCommandWorkmode {
		KsmCommandWorkmode ParsedValue { get; }
		int RawValue { get; }
	}
}